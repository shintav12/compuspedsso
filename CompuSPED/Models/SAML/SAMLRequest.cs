using System.Xml.Serialization;

namespace CompuSPED.Models.SAML
{
	[XmlRoot(ElementName = "Attribute")]
	public class Attribute
	{
		[XmlElement(ElementName = "AttributeValue")]
		public string AttributeValue { get; set; }
		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }
	}

	[XmlRoot(ElementName = "AttributeStatement")]
	public class AttributeStatement
	{
		[XmlElement(ElementName = "Attribute")]
		public Attribute Attribute { get; set; }
	}

	[XmlRoot(ElementName = "AuthnRequest")]
	public class SAMLRequest
	{
		[XmlElement(ElementName = "Issuer")]
		public string Issuer { get; set; }
		[XmlAttribute(AttributeName = "ID")]
		public string ID { get; set; }
		[XmlAttribute(AttributeName = "IssueInstant")]
		public string IssueInstant { get; set; }
		[XmlAttribute(AttributeName = "AssertionConsumerServiceURL")]
		public string AssertionConsumerServiceURL { get; set; }
		[XmlElement(ElementName = "AttributeStatement")]
		public AttributeStatement AttributeStatement { get; set; }
	}
}