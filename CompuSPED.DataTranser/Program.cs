using CompuSPED.Common.Lumen;
using CompuSPED.Common.Lumen.Responses;
using CompuSPED.DataTranser.Databases.CSPED;
using CompuSPED.DataTranser.Models.Lumen.Schools;
using CompuSPED.DataTranser.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace CompuSPED.DataTranser
{
    class Program
    {
        public readonly string baseUri = "https://wcsec-ks.compusped.com/ims/oneroster/v1p1";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        static async System.Threading.Tasks.Task Main()
        {
            var client = new HttpClient();
            string baseUri = "https://wcsec-ks.compusped.com/ims/oneroster/v1p1";
            var byteArray = Encoding.ASCII.GetBytes("TLyp8Tm4kbU7kJZvz3qXgVhjLd8NrtQe:UbIDCTGyoAACJQgNWK3Fdn4LLgVORwEK");
            var dict = new Dictionary<string, string>();
            dict.Add("grant_type", "client_credentials");
            dict.Add("scope", "https://purl.imsglobal.org/spec/or/v1p1/scope/roster.readonly https://purl.imsglobal.org/spec/or/v1p1/scope/roster-demographics.readonly https://purl.imsglobal.org/spec/or/v1p1/scope/resource.readonly https://purl.imsglobal.org/spec/or/v1p1/scope/roster.readonly https://purl.imsglobal.org/spec/or/v1p1/scope/gradebook.readonly");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11
                                                   | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            var req = new HttpRequestMessage(HttpMethod.Post, $"{baseUri}/oauth/token") { Content = new FormUrlEncodedContent(dict) };

            var response = client.SendAsync(req).Result;
            var result = response.Content.ReadAsAsync<AccessToken>().Result;

            string uri = $"{baseUri}/schools";

            var resultSchool = await uri.GetAsync<SchoolResponse>(uri, result.access_token);
            
            var dbSPED = new CSPEDContext();
            foreach (var orgs in resultSchool.orgs)
            {
                School sch = new School()
                {
                    DistrcitId = "1",
                    SchoolCode = "2",
                    SchoolName = orgs.name,
                    SchoolSourceId = orgs.sourcedId,
                    SchoolSBId = 1
                };

                dbSPED.Schools.Add(sch);
                dbSPED.SaveChanges();
                Console.WriteLine(orgs.name);
            }


            try
            {

            }
            catch (Exception ex)   // Kabiddle effen Boom man.
            {
                //string strEmailMessage = DateTime.Now.ToString() + " - There has been an exception in SFTP File Transfer." + "\r\n\r\n";
                //strEmailMessage += "Message: " + ex.Message.ToString().Trim() + "\r\n\r\n";
                //strEmailMessage += "Stack Trace: " + ex.StackTrace.ToString().Trim() + "\r\n\r\n";
                //string strErrorFilename = ConfigurationManager.AppSettings["ErrorFileName"].ToString().Trim();
                //File.AppendAllText(strErrorFilename, strEmailMessage);
                //try
                //{
                //    Notification.SendEmailException(ex);
                //}
                //catch (Exception ex2)
                //{
                //    Notification.LogError("Email error send failed. " + ex2.Message.ToString().Trim() + "\n\n" + ex2.StackTrace.ToString().Trim());
                //}
            }

        }
    }
}
