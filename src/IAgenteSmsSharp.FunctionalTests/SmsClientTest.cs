using System;
using System.Configuration;
using HelperSharp;
using NUnit.Framework;
using TestSharp;

namespace IAgenteSmsSharp.FunctionalTests
{
    [TestFixture]
    public class SmsClientTest
    {
        #region Operações que não consomem créditos
        [Test]
        public void Constructor_NullOrEmptyArgs_Exception()
        {
            // baseUrl.
            ExceptionAssert.IsThrowing(new ArgumentNullException("baseUrl"), () =>
            {
                new SmsClient(null, "userName", "password");
            });

            ExceptionAssert.IsThrowing(new ArgumentException("Argument 'baseUrl' can't be empty.", "baseUrl"), () =>
            {
                new SmsClient("", "userName", "password");
            });

            // userName.
            ExceptionAssert.IsThrowing(new ArgumentNullException("userName"), () =>
            {
                new SmsClient("baseUrl", null, "password");
            });

            ExceptionAssert.IsThrowing(new ArgumentException("Argument 'userName' can't be empty.", "userName"), () =>
            {
                new SmsClient("baseUrl", "", "password");
            });

            // password.
            ExceptionAssert.IsThrowing(new ArgumentNullException("password"), () =>
            {
                new SmsClient("baseUrl", "userName", null);
            });

            ExceptionAssert.IsThrowing(new ArgumentException("Argument 'password' can't be empty.", "password"), () =>
            {
                new SmsClient("baseUrl", "userName", "");
            });
        }

        [Test]
        public void GetMessageStatus_ValidExternalIdNotFound_MessageStatus()
        {
            var target = CreateValidClient();
            var actual = target.GetMessageStatus(int.MaxValue);
            Assert.IsNotNull(actual);
            Assert.AreEqual("ERRO", actual.Code);
            Assert.AreEqual("codigo de mensagem inexistente", actual.Description);
        }

        [Test]
        public void SendMessage_Null_Exception()
        {
            var target = CreateValidClient();

            ExceptionAssert.IsThrowing(new ArgumentNullException("message"), () =>
            {
                target.SendMessage(null);
            });
        }

        [Test]
        public void SendMessage_EmptyReceivers_Exception()
        {
            var target = CreateValidClient();

            ExceptionAssert.IsThrowing(new ArgumentException("Message needs at least one receiver to be sent.", "message"), () =>
            {
                target.SendMessage(new Message());
            });
        }

        [Test]
        public void SendMessage_NullOrEmptyText_Exception()
        {
            var target = CreateValidClient();
            var msg = new Message();
            msg.Receivers.Add("519999999");

            ExceptionAssert.IsThrowing(new ArgumentException("Message needs a text to be sent.", "message"), () =>
            {
                target.SendMessage(msg);
            });
        }

        [Test]
        public void SendMessage_ValidMessageWrongCredentials_Excetion()
        {
            var target = new SmsClient(
                "http://www.iagentesms.com.br/webservices/http.php",
                "xpto",
                "0000");

            var msg = new Message();
            msg.ExternalId = 1;
            msg.Receivers.Add(GetReceivers()[0]);
            msg.Text = "SendMessage_ValidMessageWithOneReceiver_Sent";

            ExceptionAssert.IsThrowing(new SmsException("Falha de autenticacao"), () =>
            {
                target.SendMessage(msg);
            });
        }
        #endregion

        #region Operações que consomem créditos (devem estar como Ignore como padrão).
        [Ignore] // Ignorada, pois essa operação consome créditos e IAgenteSms ainda não possui um ambiente de sandbox.
        [Test]
        public void SendMessage_ValidMessageWithOneReceiver_Sent()
        {
            var target = CreateValidClient();
            var msg = new Message();
            msg.ExternalId = 1;
            msg.Receivers.Add(GetReceivers()[0]);
            msg.Text = "Teste funcional do IAgenteSmsSharp envio simples para 1 destinatário - {0:dd/MM/yy HH:mm}".With(DateTime.Now);

            target.SendMessage(msg);

            var status = target.GetMessageStatus(msg.ExternalId.Value);
            Assert.IsNotNull(status);
            Assert.AreNotEqual("ERRO", status.Code);
        }

        [Ignore] // Ignorada, pois essa operação consome créditos e IAgenteSms ainda não possui um ambiente de sandbox.
        [Test]
        public void SendMessage_ValidMessageWithTwoReceiver_Sent()
        {
            var target = CreateValidClient();
            var msg = new Message();
            msg.ExternalId = 1;
            var receivers = GetReceivers();

            msg.Receivers.Add(receivers[0]);
            msg.Receivers.Add(receivers[1]);
            msg.Text = "Teste funcional do IAgenteSmsSharp envio em lote para 2 destinatários - {0:dd/MM/yy HH:mm}".With(DateTime.Now);

            target.SendMessage(msg);

            var status = target.GetMessageStatus(msg.ExternalId.Value);
            Assert.IsNotNull(status);
            Assert.AreNotEqual("ERRO", status.Code);
        }
        #endregion

        #region Helpers
        private static SmsClient CreateValidClient()
        {
            var userName = ConfigurationManager.AppSettings["userName"];

            if (String.IsNullOrEmpty(userName))
            {
                throw new InvalidOperationException("Antes de executar os testes funcionais, você deve configurar seu usuário e senha da IAgente no arquivo App.config.");
            }

            var password = ConfigurationManager.AppSettings["password"];

            return new SmsClient(
                "http://www.iagentesms.com.br/webservices/http.php",
                userName,
                password);
        }

        private static string[] GetReceivers()
        {
            return ConfigurationManager.AppSettings["receivers"].Split(',');
        }
        #endregion
    }
}
