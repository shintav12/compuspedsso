using CompuSPED.Models;
using CompuSPED.Models.AuthModels;
using CompuSPED.Service;
using CompuSPED.Service.Lumen;
using CompuSPED.Service.SB;
using CompuSPED.Utils;
using CompuSPED.Utils.SAML;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CompuSPED.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly UserService _userService;
        private readonly UserSBService _userSBService;
        private readonly ClientService _clientService;
        private static readonly GlobalSettings GlobalSettings = GlobalSettings.GetGlobalSettings();
        public AuthorizationController(UserService userService, UserSBService userSBService, ClientService clientService)
        {
            this._userService = userService;
            this._userSBService = userSBService;
            this._clientService = clientService;
        }

        public async Task<ActionResult> OAuthAutorize(string client_id, string grant_type ="", string redirect_uri = "", string state = "", string client_secret = "", string username = "")   
        {

            if (Session["identity"] != null) return Redirect($"{GlobalSettings.RedirectionSBURL}?identityRequest={Session["identity"]}");
            var response = await _clientService.ValidateClientId(client_id);
            
            if (response.HasError)
            {
                ViewBag.PageError = true;
                ViewBag.Message = "Client Credentials Invalid";
                return View("LoginForm");
            }

            Session["state"] = state;
            Session["client_id"] = client_id;

            if (grant_type == "client_credentials") 
                    return Redirect($"credentials?client_id={client_id}&client_secret={client_secret}&username={username}");


            ViewBag.Error = false;
            ViewBag.PageError = false;
            ViewBag.Message = "";
            ViewBag.RedirectUri = redirect_uri;
            ViewBag.ClientId = client_id;

            return View("LoginForm",new LoginModel { client_id = client_id, redirect_uri = redirect_uri });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = true;
                ViewBag.PageError = false;
                ViewBag.Message = "Invalid Input";
                return RedirectToAction("OAuthAutorize", new { client_id = model.client_id, redirect_uri = model.redirect_uri, state = Session["state"].ToString() });
            }

            var response = await _userService.AuthUser(model.email, model.password);
            if (response.HasError)
            {
                model.email = "";
                model.password = "";
                ViewBag.Error = true;
                ViewBag.PageError = false;
                ViewBag.Message = response.ErrorMessage;
                return RedirectToAction("OAuthAutorize", new { client_id = model.client_id, redirect_uri = model.redirect_uri, state = Session["state"].ToString() });
            }

            var validateUser = await _userSBService.ValidateNewUser(response.Result);
            if (validateUser.HasError)
            {
                model.email = "";
                model.password = "";
                ViewBag.Error = true;
                ViewBag.PageError = false;
                ViewBag.Message = validateUser.ErrorMessage;
                return RedirectToAction("OAuthAutorize", new { client_id = model.client_id, redirect_uri = model.redirect_uri, state = Session["state"].ToString() });
            }
            string code = Guid.NewGuid().ToString();
            Session["code"] = code;
            Session["user"] = response.Result.email;
            return Redirect($"{model.redirect_uri}?code={code}");
        }

        public async Task<ActionResult> CredentialsValidation(string client_id, string client_secret, string username)
        {
            if (Session["identity"] != null) return Redirect($"{GlobalSettings.RedirectionSBURL}?identityRequest={Session["identity"]}");
            var response = await _clientService.ValidateClientIdAndSecret(client_id, client_secret);
            if (response.HasError)
            {
                ViewBag.PageError = true;
                ViewBag.Message = "Client Credentials Invalid";
                return View("LoginForm");
            }
            var userResponse = await _userService.ValidateUserEmail(username);
            if (userResponse.HasError)
            {
                ViewBag.PageError = true;
                ViewBag.Message = "Client Credentials Invalid";
                return View("LoginForm");
            }

            var validateUser = await _userSBService.ValidateNewUser(userResponse.Result);
            if (validateUser.HasError)
            {
                ViewBag.PageError = true;
                ViewBag.Message = "Client Credentials Invalid";
                return View("LoginForm");
            }
            var identityResponse = new IdentityResponse { ClientId = response.Result.ClientCode, Username = validateUser.Result.Email };
            var xmlSerialize = XMLHelper.XmlSerializeFromObject(identityResponse);
            var encoded = XMLHelper.DeflateAndEncode(xmlSerialize);
            var urlEncoded = HttpUtility.UrlEncode(encoded);
            Session["identity"] = urlEncoded;
            return Redirect($"{GlobalSettings.RedirectionSBURL}?identityRequest={urlEncoded}");
        }

        public async Task<ActionResult> ValidateCredentials(string client_id, string client_secret, string code, string state)
        {
            if (Session["identity"] != null) return Redirect($"{GlobalSettings.RedirectionSBURL}?identityRequest={Session["identity"]}");
            var response = await _clientService.ValidateClientIdAndSecret(client_id, client_secret);
            if (response.HasError)
            {
                ViewBag.PageError = true;
                ViewBag.Message = "Client Credentials Invalid";
                return View("LoginForm");
            }
            string sesCode = Session["code"].ToString();
            if (!sesCode.Equals(code))
            {
                ViewBag.PageError = true;
                ViewBag.Message = "Invalid request";
                return View("LoginForm");
            }
            string sesState = Session["state"].ToString();
            if (!sesState.Equals(state))
            {
                ViewBag.PageError = true;
                ViewBag.Message = "Invalid request";
                return View("LoginForm");
            }
            string email = Session["user"].ToString();
            var identityResponse = new IdentityResponse { ClientId = response.Result.ClientCode, Username = email };
            var xmlSerialize = XMLHelper.XmlSerializeFromObject(identityResponse);
            var encoded = XMLHelper.DeflateAndEncode(xmlSerialize);
            var urlEncoded = HttpUtility.UrlEncode(encoded);
            Session["identity"] = urlEncoded;
            return Redirect($"{GlobalSettings.RedirectionSBURL}?identityRequest={urlEncoded}");
        }


        public async Task<ActionResult> ValidateCode(string client_id, string client_secret, string code, string state)
        {
            if (Session["identity"] != null) return Redirect($"{GlobalSettings.RedirectionSBURL}?identityRequest={Session["identity"]}");
            var response = await _clientService.ValidateClientIdAndSecret(client_id, client_secret);
            if (response.HasError)
            {
                ViewBag.PageError = true;
                ViewBag.Message = "Client Credentials Invalid";
                return View("LoginForm");
            }
            string sesCode = Session["code"].ToString();
            if (!sesCode.Equals(code))
            {
                ViewBag.PageError = true;
                ViewBag.Message = "Invalid request";
                return View("LoginForm");
            }
            string sesState = Session["state"].ToString();
            if (!sesState.Equals(state))
            {
                ViewBag.PageError = true;
                ViewBag.Message = "Invalid request";
                return View("LoginForm");
            }
            string email = Session["user"].ToString();
            var identityResponse = new IdentityResponse { ClientId = response.Result.ClientCode, Username = email };
            var xmlSerialize = XMLHelper.XmlSerializeFromObject(identityResponse);
            var encoded = XMLHelper.DeflateAndEncode(xmlSerialize);
            var urlEncoded = HttpUtility.UrlEncode(encoded);
            Session["identity"] = urlEncoded;
            return Redirect($"{GlobalSettings.RedirectionSBURL}?identityRequest={urlEncoded}");
        }
    }
}
