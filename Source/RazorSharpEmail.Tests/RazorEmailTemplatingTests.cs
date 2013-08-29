using System;
using Email.Models;
using FluentAssertions;
using NUnit.Framework;

namespace RazorSharpEmail.Tests {
    [TestFixture]
    public class RazorEmailTemplatingTests {
        private IEmailGenerator _emailGenerator;

        [TestFixtureSetUp]
        public void TestFixtureSetUp() {
            _emailGenerator = Given.AnInitializedEmailFormatter();
        }

        [Test]
        public void Should_throw_when_in_language_scope_and_cant_find_template() {
            using (new LanguageScope("pl-PL")) {
                Assert.Throws<ArgumentException>(() => _emailGenerator.Generate(new Welcome()));
            }
        }

        [Test]
        public void Given_an_email_formatter_when_attempting_to_format_a_message_using_the_default_language_it_should_not_throw() {
            try {
                LanguageScope.SetDefaultLanguage("en-AU");
                _emailGenerator.Generate(new Welcome());
            } finally {
                LanguageScope.ClearDefaultLanguage();
            }
        }

        [Test]
        public void Should_populate_email_model() {
            using (new LanguageScope("en-AU")) {
                var email = _emailGenerator.Generate(
                    new Welcome {
                        FirstName = "Michael",
                        Message = "Hello World!",
                        Url = "http://google.com"
                    });

                email.Should().NotBeNull();
                email.Subject.Should().NotBeBlank();
                email.PlainTextBody.Should().NotBeBlank();
                email.HtmlBody.Should().NotBeBlank();
            }
        }

        [Test]
        public void Can_generate_by_name() {
            var email = _emailGenerator.Generate(
                new Welcome {
                    FirstName = "Michael",
                    Message = "Hello World!",
                    Url = "http://google.com"
                }, "Welcome");

            email.Should().NotBeNull();
            email.Subject.Should().NotBeBlank();
            email.PlainTextBody.Should().NotBeBlank();
            email.HtmlBody.Should().NotBeBlank();
        }

        [Test]
        public void Can_generate_from_files() {
            var emailGenerator = new RazorEmailGenerator("Templates");
            var email = emailGenerator.Generate(
                new Welcome {
                    FirstName = "Michael",
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
