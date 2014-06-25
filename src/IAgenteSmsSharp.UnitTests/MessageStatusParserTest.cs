using System;
using NUnit.Framework;
using TestSharp;

namespace IAgenteSmsSharp.UnitTests
{
    [TestFixture]
    public class MessageStatusParserTest
    {
        [Test]
        public void Parse_NullOrEmptyContent_Exception()
        {
            var target = new MessageStatusParser();

            ExceptionAssert.IsThrowing(new ArgumentNullException("content"), () =>
            {
                target.Parse(null);
            });

            ExceptionAssert.IsThrowing(new ArgumentException("Argument 'content' can't be empty.", "content"), () =>
            {
                target.Parse("");
            });
        }

        [Test]
        public void Parse_InvalidContent_Exception()
        {
            var target = new MessageStatusParser();

            ExceptionAssert.IsThrowing(new FormatException("The content '01234' can't be parsed as a MessageStatus."), () =>
            {
                target.Parse("01234");
            });
        }

        [Test]
        public void Parse_ValidContent_MessageStatus()
        {
            var target = new MessageStatusParser();

            var actual = target.Parse("Erro - codigo de mensagem inexistente");
            Assert.IsNotNull(actual);
            Assert.AreEqual("ERRO", actual.Code);
            Assert.AreEqual("codigo de mensagem inexistente", actual.Description);

            actual = target.Parse("Aguardando");
            Assert.IsNotNull(actual);
            Assert.AreEqual("AGUARDANDO", actual.Code);
            Assert.IsTrue(String.IsNullOrEmpty(actual.Description));
        }
    }
}
