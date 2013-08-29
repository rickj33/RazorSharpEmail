using System;
using System.IO;
using RazorEngine.Templating;

namespace RazorSharpEmail {
    /// <summary>
    /// Resolves templates from a root folder.
    /// </summary>
    public class FolderTemplateResolver : ITemplateResolver {
        private readonly string _rootFolder;

        /// <summary>
        /// Specify the root folder path to resolve templates from.
        /// </summary>
        /// <param name="rootFolder">The assembly where the templates are embedded.</param>
        public FolderTemplateResolver(string rootFolder = null) {
            _rootFolder = rootFolder != null ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, rootFolder) : AppDomain.CurrentDomain.BaseDirectory;
        }

        public string Resolve(string name) {
            if (name == null)
                throw new ArgumentNullException("name");

            return File.ReadAllText(Path.Combine(_rootFolder, name));
        }
    }
}
