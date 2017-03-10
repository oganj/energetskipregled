using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using EnergetskiPregled.Configuration;
using EnergetskiPregled.Contracts.Service;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace EnergetskiPregled.Services
{
	// This class is used by the application to send Email and SMS
	// when you turn on two-factor authentication in ASP.NET Identity.
	// For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
	public class AuthMessageSender : IEmailSender, ISmsSender
	{
		private IOptions<MailConfiguration> _config;
		private readonly string _host;
		private readonly int _port;
		private readonly string _user;
		private readonly string _pass;
		private readonly bool _ssl;
		private readonly string _sender;

		public AuthMessageSender(IOptions<MailConfiguration> config)
		{
			if (_config == null)
				_config = config;

			_host = config.Value.MailServer;
			_port = config.Value.Port;
			_user = config.Value.MailAuthUser;
			_pass = config.Value.MailAuthPass;
			_ssl = config.Value.EnableSSL;
			_sender = config.Value.EmailFromAddress;
		}

		public async Task SendEmailAsync(string email, string subject, string message)
		{
			bool isEmailSent = false;
			int retryCount = 0;

			// Try to send email 3 times. This is if throttling error occurs.
			while (!isEmailSent && retryCount < 3)
			{
				// Send the email.
				try
				{
					retryCount++;
					await SendEmailSmtpAsync(email, subject, message + _config.Value.Signature);
					isEmailSent = true;
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error message: " + ex.Message);
					// Try again after 1 second.
					Thread.Sleep(1000);
				}
			}
		}

		public Task SendSmsAsync(string number, string message)
		{
			// Plug in your SMS service here to send a text message.
			return Task.FromResult(0);
		}

		#region Private
		private async Task SendEmailSmtpAsync(string recipient, string subject, string body)
		{
			BodyBuilder bodyBuilder = new BodyBuilder();
			bodyBuilder.HtmlBody = body;

			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("EnergetskiPregled", _sender));
			message.To.Add(new MailboxAddress("", recipient));
			message.Subject = subject;
			message.Body = bodyBuilder.ToMessageBody();

			using (var client = new SmtpClient())
			{
				await client.ConnectAsync(_host, _port, SecureSocketOptions.StartTlsWhenAvailable).ConfigureAwait(false);
				client.AuthenticationMechanisms.Remove("XOAUTH2");
				await client.AuthenticateAsync(_user, _pass).ConfigureAwait(false);
				await client.SendAsync(message).ConfigureAwait(false);
				await client.DisconnectAsync(true).ConfigureAwait(false);
			}
		}
		#endregion
	}
}
