using System.IO;
using ApprovalTests.Reporters;
using Email.Models;
using FluentAssertions;
using NUnit.Framework;

namespace RazorSharpEmail.Tests
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter))]
    public class EmailFormattingTests
    {
        private IEmailGenerator _emailGenerator;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _emailGenerator = Given.AnInitializedEmailFormatter();
        }

        [Test]
        public void Should_format_email_from_model()
        {
            var email = _emailGenerator.Generate(
                new Welcome
                {
                    FirstName = "Michael",
                    Message = "Hello World!",
                    Url = "http://google.com"
                });
            
            ApprovalTests.Approvals.Verify(email.Everything());
        }

        [Test]
        public void Should_inline_styles() {
            var email = _emailGenerator.Generate(new object(), "Simple");

            var premailer = new PreMailer.Net.PreMailer();
            email.HtmlBody = premailer.MoveCssInline(email.HtmlBody, false);

            ApprovalTests.Approvals.Verify(email.HtmlBody);
        }
    }
}