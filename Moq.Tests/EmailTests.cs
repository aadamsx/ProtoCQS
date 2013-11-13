using System;
using AspNetIdentity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcWeb.Controllers;
using Postal;

namespace Moq.Tests
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void action_sends_email()
        {
            var emailService = new Moq.Mock<IEmailService>();
            var webSecurity = new Moq.Mock<IWebSecurity>();

            var emailserv = new EmailService(viewEngines: null);

            var controller = new AccountController(webSecurity.Object, emailService.Object);

            dynamic sentEmail = null;

            emailService
                .Setup(s => s.Send(It.IsAny<Email>()))
                .Callback<Email>(e => sentEmail = e);

            //controller.Send(new HelpRequest
            //{
            //    EmailAddress = "john@smith.com",
            //    Message = "Help me!",
            //    Name = "John Smith"
            //});

            Assert.IsNotNull(sentEmail, "Email message was not sent.");

            Assert.AreEqual("john@smith.com", sentEmail.UserEmailAddress);
            Assert.AreEqual("Help me!", sentEmail.Message);
            Assert.AreEqual("John Smith", sentEmail.Name);
            Assert.IsNotNull(sentEmail.TicketId);
        }
    }
}
