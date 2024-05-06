using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace CompuSPED.Utils.SAML
{
    public static class XMLHelper
    {
        public static T XmlDeserializeFromString<T>(string objectData)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(objectData))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        public static string DecodeAndInflate(string str)
        {
            var utf8 = Encoding.UTF8;
            var bytes = Convert.FromBase64String(str);
            using (var output = new MemoryStream())
            {
                using (var input = new MemoryStream(bytes))
                {
                    using (var unzip = new DeflateStream(input, CompressionMode.Decompress))
                    {
                        unzip.CopyTo(output, bytes.Length);
                        unzip.Close();
                    }
                    return utf8.GetString(output.ToArray());
                }
            }
        }

        public static string DeflateAndEncode(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            using (var output = new MemoryStream())
            {
                using (var zip = new DeflateStream(output, CompressionMode.Compress))
                {
                    zip.Write(bytes, 0, bytes.Length);
                }
                var base64 = Convert.ToBase64String(output.ToArray());

                return base64;
            }
        }

        public static string XmlSerializeFromObject<T>(T data)
        {
            using (var stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stringwriter, data);
                return stringwriter.ToString();
            }
        }

        public static string DecodeSAMLRequestString(string compressedData)
        {
            var memStream = new MemoryStream(Convert.FromBase64String(WebUtility.UrlDecode(compressedData ?? string.Empty)));
            var deflate = new DeflateStream(memStream, CompressionMode.Decompress);
            string decodedString = new StreamReader(deflate, Encoding.UTF8).ReadToEnd();

            return decodedString;
        }

        public static bool IsBase64String(string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }
    }
}