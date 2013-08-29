using Email.Models;

namespace RazorSharpEmail.Tests
{
    public static class Given
    {
    	private static IEmailFormatter _initializedEmailFormatter;

        public static IEmailFormatter AnInitializedEmailFormatter()
        {
			if (_initializedEmailFormatter == null)
                _initializedEmailFormatter = new RazorEmailFormatter(typeof(SimpleEmail));

            return _initializedEmailFormatter;
        }
    }
}