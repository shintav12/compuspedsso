using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CompuSPED.Models.SAML
{
	[XmlRoot(ElementName = "Status")]
	public class Status
	{
		[XmlElement(ElementName = "StatusCode")]
		public string StatusCode { get; set; }
	}

	[XmlRoot(ElementName = "SubjectConfirmationData")]
	public class SubjectConfirmationData
	{
		[XmlAttribute(AttributeName = "InResponseTo")]
		public string InResponseTo { get; set; }
		[XmlAttribute(AttributeName = "NotOnOrAfter")]
		public string NotOnOrAfter { get; set; }
		[XmlAttribute(AttributeName = "Recipient")]
		public string Recipient { get; set; }
	}

	[XmlRoot(ElementName = "SubjectConfirmation")]
	public class SubjectConfirmation
	{
		[XmlElement(ElementName = "SubjectConfirmationData")]
		public SubjectConfirmationData SubjectConfirmationData { get; set; }
	}

	[XmlRoot(ElementName = "Subject")]
	public class Subject
	{
		[XmlElement(ElementName = "NameID")]
		public string NameID { get; set; }
		[XmlElement(ElementName = "SubjectConfirmation")]
		public SubjectConfirmation SubjectConfirmation { get; set; }
	}

	[XmlRoot(ElementName = "Conditions")]
	public class Conditions
	{
		[XmlElement(ElementName = "Audience")]
		public string Audience { get; set; }
		[XmlAttribute(AttributeName = "NotBefore")]
		public string NotBefore { get; set; }
		[XmlAttribute(AttributeName = "NotOnOrAfter")]
		public string NotOnOrAfter { get; set; }
	}

	[XmlRoot(ElementName = "AuthContext")]
	public class AuthContext
	{
		[XmlElement(ElementName = "AuthContextClassRef")]
		public string AuthContextClassRef { get; set; }
	}

	[XmlRoot(ElementName = "AuthStatement")]
	public class AuthStatement
	{
		[XmlElement(ElementName = "SubjectLocality")]
		public string SubjectLocality { get; set; }
		[XmlElement(ElementName = "AuthContext")]
		public AuthContext AuthContext { get; set; }
		[XmlAttribute(AttributeName = "AuthInstant")]
		public string AuthInstant { get; set; }
		[XmlAttribute(AttributeName = "SessionIndex")]
		public string SessionIndex { get; set; }
		[XmlAttribute(AttributeName = "SessionNotOnOrAfter")]
		public string SessionNotOnOrAfter { get; set; }
	}

	[XmlRoot(ElementName = "Assertion")]
	public class Assertion
	{
		[XmlElement(ElementName = "Issuer")]
		public string Issuer { get; set; }
		[XmlElement(ElementName = "Signature")]
		public string Signature { get; set; }
		[XmlElement(ElementName = "Subject")]
		public Subject Subject { get; set; }
		[XmlElement(ElementName = "Conditions")]
		public Conditions Conditions { get; set; }
		[XmlElement(ElementName = "AuthStatement")]
		public AuthStatement AuthStatement { get; set; }
		[XmlElement(ElementName = "AttributeStatement")]
		public AttributeStatement AttributeStatement { get; set; }
		[XmlAttribute(AttributeName = "ID")]
		public string ID { get; set; }
		[XmlAttribute(AttributeName = "IssueInstant")]
		public string IssueInstant { get; set; }
		[XmlAttribute(AttributeName = "Version")]
		public string Version { get; set; }
	}

	[XmlRoot(ElementName = "Response")]
	public class SAMLResponse
	{
		[XmlElement(ElementName = "Issuer")]
		public string Issuer { get; set; }
		[XmlElement(ElementName = "Status")]
		public Status Status { get; set; }
		[XmlElement(ElementName = "Assertion")]
		public Assertion Assertion { get; set; }
		[XmlAttribute(AttributeName = "ID")]
		public string ID { get; set; }
		[XmlAttribute(AttributeName = "InResponseTo")]
		public string InResponseTo { get; set; }
		[XmlAttribute(AttributeName = "IssueInstant")]
		public string IssueInstant { get; set; }
		[XmlAttribute(AttributeName = "Version")]
		public string Version { get; set; }
	}
}