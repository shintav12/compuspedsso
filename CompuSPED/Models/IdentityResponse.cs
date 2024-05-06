using System.Xml.Serialization;

namespace CompuSPED.Models
{
    [XmlRoot(ElementName = "IdentityResponse")]
	public class IdentityResponse
	{
		[XmlElement(ElementName = "Username")]
		public string Username { get; set; }
		[XmlElement(ElementName = "ClientId")]
		public string ClientId { get; set; }
	}
}