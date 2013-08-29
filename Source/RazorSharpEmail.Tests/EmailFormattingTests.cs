using ApprovalTests.Reporters;
using Email.Models;
using NUnit.Framework;

namespace RazorSharpEmail.Tests
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter))]
    public class EmailFormattingTests
    {
        private IEmailFormatter _emailFormatter;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _emailFormatter = Given.AnInitializedEmailFormatter();
        }

        [Test]
        public void Should_format_email_from_modal()
        {
            var email = _emailFormatter.BuildTemplatedEmailFrom(
                new SimpleEmail
                {
                    RecipientFirstName = "Michael",
                    ReferenceNumber = "REF123456",
                    Message = "Hello World!",
                    Url = "http://google.com"
                });
            
            ApprovalTests.Approvals.Verify(email.Everything());
        }
    }
}