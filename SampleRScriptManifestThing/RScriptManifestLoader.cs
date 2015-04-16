using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

using Coolblue.Utils;

namespace SampleRScriptManifestThing
{
    public class RScriptManifestLocator
    {
        private readonly Assembly _assembly;

        public RScriptManifestLocator(Assembly assembly)
        {
            _assembly = assembly;
        }

        public IEnumerable<String> GetAllRScriptResources(string pathFilter = null)
        {
            pathFilter = pathFilter ?? String.Empty;

            var regexFilter = String.Format(".*\\.{0}.*\\.r", pathFilter.Replace("\\", ".").Replace(".", "\\."));

            var rResourceNames = _assembly.GetManifestResourceNames()
                .Where(resourceName => Regex.IsMatch(resourceName, regexFilter, RegexOptions.IgnoreCase));

            var fileReader = new TextResourceReader(_assembly, pathFilter);
            var assemblyNameAndNestedPath = Path.Combine(_assembly.GetName().Name, pathFilter).Replace('\\', '.');

            return rResourceNames
                .Select(rResourceName => rResourceName.Replace(assemblyNameAndNestedPath, String.Empty).Trim('.'))
                .Select(fileReader.Read);
        }
    }
}
