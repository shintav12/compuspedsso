using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System;
using CompuSPED.Common.Lumen;
using CompuSPED.Common.Lumen.Users;
using CompuSPED.Common.Lumen.Responses;
using CompuSPED.DataTranser.Utils;
using System.Globalization;
using CompuSPED.DataTranser.Databases.GB;
using System.Linq;
using CompuSPED.DataTranser.Databases.CSPED;

namespace CompuSPED.DataTranser
{
    public  class StudentDataTransfer
    {
        public readonly string baseUri = "https://gg4l-sandbox2.lumentouchhosts.com/ims/oneroster/v1p1";
        public void StudentTransfer()
        {
            var dbGB = new GBContext();
            var dbSPED = new CSPEDContext();
            var token = APIAuth();
            if(token == String.Empty)
            {
                throw new Exception("Lumen Auth Failed");
            }
            var user = GetLumenStudents(token);
            if(user == null)
            {
                throw new Exception("Error with Service");
            }

            foreach (var userItem in user)
            {
                var date = DateTime.ParseExact(userItem.dateLastModified,
                                       "yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'",
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.AssumeUniversal |
                                       DateTimeStyles.AdjustToUniversal);
                if (date.Date.AddDays(1) != DateTime.Now.Date) continue;
                var GBStudent = dbGB.Students.FirstOrDefault(x => x.UIC.Equals(userItem.identifier));
                var studentDemographic = GetStudentDemograpchi(token, userItem.sourcedId);
                if(GBStudent == null)
                {
                    GBStudent = new Student 
                    { 
                        UIC = userItem.identifier,
                        ActiveFlag = true,
                        DistrictID = dbSPED.Districts.First(x => x.DistrictSourceId.Equals(userItem.orgs.First(y=> y.type.Equals("District")).sourcedId)).DistrictSBId,
                        SchoolID = dbSPED.Schools.First(x => x.SchoolSourceId.Equals(userItem.orgs.First(y => y.type.Equals("School")).sourcedId)).SchoolSBId,
                        FirstName = userItem.givenName,
                        Gender = studentDemographic.sex,
                        Birthdate = DateTime.ParseExact(studentDemographic.birthDate, "yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal),
                        City = studentDemographic.cityOfBirth,
                        LastName = userItem.familyName,
                        
                        MiddleInitial = userItem.middleName,
                        Phone = userItem.sms
                    };
                    dbGB.Students.Add(GBStudent);
                }
                else
                {
                    GBStudent.UIC = userItem.identifier;
                    GBStudent.ActiveFlag = true;
                    GBStudent.DistrictID = dbSPED.Districts.First(x => x.DistrictSourceId.Equals(userItem.orgs.First(y => y.type.Equals("District")).sourcedId)).DistrictSBId;
                    GBStudent.SchoolID = dbSPED.Schools.First(x => x.SchoolSourceId.Equals(userItem.orgs.First(y => y.type.Equals("School")).sourcedId)).SchoolSBId;
                    GBStudent.FirstName = userItem.givenName;
                    GBStudent.Gender = studentDemographic.sex;
                    GBStudent.Birthdate = DateTime.ParseExact(studentDemographic.birthDate, "yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
                    GBStudent.City = studentDemographic.cityOfBirth;
                    GBStudent.LastName = userItem.familyName;
                    GBStudent.MiddleInitial = userItem.middleName;
                    GBStudent.Phone = userItem.sms;
                }
                dbGB.SaveChanges();
            }

        }

        private string APIAuth()
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes("kJ2BBwKXnRnQDWi9r4MKmyEcDPaBIyd5:ZX610e6mAubZ2adiM4Xu6T6HS7423J8d");
                var dict = new Dictionary<string, string>();
                dict.Add("grant_type", "client_credentials");
                dict.Add("scope", "https://purl.imsglobal.org/spec/or/v1p1/scope/roster.readonly https://purl.imsglobal.org/spec/or/v1p1/scope/roster-demographics.readonly https://purl.imsglobal.org/spec/or/v1p1/scope/resource.readonly https://purl.imsglobal.org/spec/or/v1p1/scope/roster.readonly https://purl.imsglobal.org/spec/or/v1p1/scope/gradebook.readonly");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11
                                                       | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                var req = new HttpRequestMessage(HttpMethod.Post, $"{baseUri}/oauth/token") { Content = new FormUrlEncodedContent(dict) };

                var response = client.SendAsync(req).Result;
                if (response.IsSuccessStatusCode)
                {
                    return String.Empty;
                }
                var result = response.Content.ReadAsAsync<AccessToken>().Result;
                return result.access_token;
            }
        }

        private List<User> GetLumenStudents(string token)
        {
            string uri = $"{baseUri}/users";
            
                var result = uri.GetAsync<GetAllUsersResponse>(uri, token).Result;
                return result.users;
        }

        private Demographics GetStudentDemograpchi(string token, string userID)
        {
            string uri = $"{baseUri}/demographics/{userID}";

            var result = uri.GetAsync<DemographicResponse>(uri, token).Result;
            return result.demographics;
        }

    }
}
