using CompuSPED.Models;
using CompuSPED.Models.AuthModels;
using CompuSPED.Models.SAML;
using CompuSPED.Service;
using CompuSPED.Service.Lumen;
using CompuSPED.Service.SB;
using CompuSPED.Utils;
using CompuSPED.Utils.SAML;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CompuSPED.Controllers.web
{
    public class SAMLAuthController : Controller
    {
        private readonly UserService _userService;
        private readonly UserSBService _userSBService;
        private readonly ClientService _clientService;

        private static readonly GlobalSettings GlobalSettings = GlobalSettings.GetGlobalSettings();

        public SAMLAuthController(UserService userService, UserSBService userSBService, ClientService clientService)
        {
            this._userService = userService;
            this._userSBService = userSBService;
            this._clientService = clientService;
        }

        public async Task<ActionResult> SAMLAuthorize(string samlRequest)
        {
            try
            {
                if (Session["identity"] != null) return Redirect($"{GlobalSettings.RedirectionSBURL}?identityRequest={Session["identity"]}");
                ViewBag.Error = false;
                ViewBag.PageError = false;
                ViewBag.Message = "";

                if (!XMLHelper.IsBase64String(samlRequest))
                {
                    ViewBag.PageError = true;
                    ViewBag.Message = "Something went wrong with your request";
                    return View("LoginForm");
                }
                string incomingUrl = HttpContext.Request.Url.Host;
                var stringXML = XMLHelper.DecodeAndInflate(samlRequest);
                var request = XMLHelper.XmlDeserializeFromString<SAMLRequest>(stringXML);
                if (!request.Issuer.Contains(incomingUrl))
                {
                    ViewBag.PageError = true;
                    ViewBag.Message = "Something went wrong with your request";
                    return View("LoginForm");
                }

                var clientResponse = await _clientService.ValidateIssuerAbdACS(request.Issuer, request.AssertionConsumerServiceURL);
                if (clientResponse.HasError)
                {
                    ViewBag.PageError = true;
                    ViewBag.Message = "Something went wrong with your request";
                    return View("LoginForm");
                }

                if(request.AttributeStatement != null) {
                    return Redirect($"validation/samlRequest?{samlRequest}");
                }

                Session["issuer"] = request.Issuer;
                Session["client_code"] = clientResponse.Result.ClientCode;
                Session["acs"] = request.AssertionConsumerServiceURL;

                return View("LoginForm");
            }
            catch (Exception)
            {

                ViewBag.PageError = true;
                ViewBag.Message = "Something went wrong with your request";
                return View("LoginForm");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginSAMLModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = true;
                ViewBag.PageError = false;
                ViewBag.Message = "Invalid Input";
                return View("LoginForm");
            }

            var response = await _userService.AuthUser(model.email, model.password);
            if (response.HasError)
            {
                model.email = "";
                model.password = "";
                ViewBag.Error = true;
                ViewBag.PageError = false;
                ViewBag.Message = response.ErrorMessage;
                return View("LoginForm", model);
            }

            var validateUser = await _userSBService.ValidateNewUser(response.Result);
            if (validateUser.HasError)
            {
                model.email = "";
                model.password = "";
                ViewBag.PageError = false;
                ViewBag.Error = true;
                ViewBag.Message = validateUser.ErrorMessage;
                return View("LoginForm", model);
            }
            var client_code = Session["client_code"];
            Session["user"] = response.Result.email;
            var identityResponse = new IdentityResponse { ClientId = client_code.ToString(), Username = validateUser.Result.Email };
            var xmlSerialize = XMLHelper.XmlSerializeFromObject(identityResponse);
            var encoded = XMLHelper.DeflateAndEncode(xmlSerialize);
            var urlEncoded = HttpUtility.UrlEncode(encoded);
            Session["identity"] = urlEncoded;
            return Redirect($"{GlobalSettings.RedirectionSBURL}?identityRequest={urlEncoded}");
        }

        public async Task<ActionResult> CredentialValidation(string samlRequest)
        {
            if (Session["identity"] != null) return Redirect($"{GlobalSettings.RedirectionSBURL}?identityRequest={Session["identity"]}");
            var stringXML = XMLHelper.DecodeAndInflate(samlRequest);
            var request = XMLHelper.XmlDeserializeFromString<SAMLRequest>(stringXML);
            if(request.AttributeStatement.Attribute.Name != "Email")
            {
                {
                    ViewBag.PageError = true;
                    ViewBag.Error = false;
                    ViewBag.Message = "Login Request Invalid";
                    return View("LoginForm");
                }
            }
            var response = await _userService.ValidateUserEmail(request.AttributeStatement.Attribute.AttributeValue);
            if (response.HasError)
            {
                ViewBag.PageError = true;
                ViewBag.Error = false;
                ViewBag.Message = response.ErrorMessage;
                return View("LoginForm");
            }

            var validateUser = await _userSBService.ValidateNewUser(response.Result);
            if (validateUser.HasError)
            {
                ViewBag.PageError = true;
                ViewBag.Error = false;
                ViewBag.Message = validateUser.ErrorMessage;
                return View("LoginForm");
            }

            var client_code = Session["client_code"];
            var identityResponse = new IdentityResponse { ClientId = client_code.ToString(), Username = validateUser.Result.Email };
            var xmlSerialize = XMLHelper.XmlSerializeFromObject(identityResponse);
            var encoded = XMLHelper.DeflateAndEncode(xmlSerialize);
            var urlEncoded = HttpUtility.UrlEncode(encoded);
            Session["identity"] = urlEncoded;

            return Redirect($"{GlobalSettings.RedirectionSBURL}?identityRequest={urlEncoded}");
        }

    }
}