using Email.Models;

namespace RazorSharpEmail.Tests
{
    public static class Given
    {
    	private static IEmailGenerator _initializedEmailGenerator;

        public static IEmailGenerator AnInitializedEmailFormatter()
        {
			if (_initializedEmailGenerator == null)
                _initializedEmailGenerator = new RazorEmailGenerator(typeof(Welcome));

            return _initializedEmailGenerator;
        }
    }
}