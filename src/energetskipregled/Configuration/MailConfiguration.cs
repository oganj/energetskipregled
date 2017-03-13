namespace EnergetskiPregled.Configuration
{
	public class MailConfiguration
	{
		public string MailServer { get; set; }
		public int Port { get; set; }
		public bool EnableSSL { get; set; }
		public string EmailFromAddress { get; set; }
		public string MailAuthUser { get; set; }
		public string MailAuthPass { get; set; }
		public string AdminMail { get; set; }
		public string Signature { get; set; }
	}
}
