using System;
using Email.Models;
using FluentAssertions;
using NUnit.Framework;

namespace RazorSharpEmail.Tests
{
    [TestFixture]
    public class RazorEmailTemplatingTests
    {
        private IEmailFormatter _emailFormatter;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _emailFormatter = Given.AnInitializedEmailFormatter();
        }

        [Test]
        public void Given_an_email_formatter_when_attempting_to_format_a_message_in_a_language_without_templates_it_should_throw()
        {
            using (new LanguageScope("pl-PL"))
            {
                Assert.Throws<ArgumentException>(() => _emailFormatter.BuildTemplatedEmailFrom(new SimpleEmail()));
            }
        }
        
        [Test]
        public void Given_an_email_formatter_when_attempting_to_format_a_message_using_the_default_language_it_should_not_throw()
        {
            try
            {
                LanguageScope.SetDefaultLanguage("en-AU");
                _emailFormatter.BuildTemplatedEmailFrom(new SimpleEmail());
            }
            finally
            {
                LanguageScope.ClearDefaultLanguage();
            }
        }

        [Test]
        public void Should_populate_email_model() {
            using (new LanguageScope("en-AU")) {
                var email = _emailFormatter.BuildTemplatedEmailFrom(
                    new SimpleEmail {
                        RecipientFirstName = "Michael",
                        ReferenceNumber = "REF123456",
                        Message = "Hello World!",
                        Url = "http://google.com"
                    });

                email.Should().NotBeNull();
                email.Subject.Should().NotBeBlank();
                email.PlainTextBody.Should().NotBeBlank();
                email.HtmlBody.Should().NotBeBlank();
            }
        }
    }
}
