using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SketchCore.Functions
{
    class ApplicationHelper
    {
        private static readonly Lazy<ApplicationHelper> _lazy = new Lazy<ApplicationHelper>(() => new ApplicationHelper());

        private ApplicationHelper()
        {
            ConfigureBindingRedirects();
        }

        public static void Configure()
        {
            // Ensure lazy initialization
            var _ = _lazy.Value;
        }

        private void ConfigureBindingRedirects()
        {
            RedirectAssembly("System.ValueTuple", new Version("4.0.2.0"), "cc7b13ffcd2ddd51");
        }

        private static void RedirectAssembly(string shortName, Version targetVersion, string publicKeyToken)
        {
            ResolveEventHandler handler = null;
            handler = (sender, args) =>
            {
                var requestedAssembly = new AssemblyName(args.Name);
                if (requestedAssembly.Name != shortName)
                {
                    return null;
                }
                var targetPublicKeyToken = new AssemblyName("x, PublicKeyToken=" + publicKeyToken).GetPublicKeyToken();
                requestedAssembly.SetPublicKeyToken(targetPublicKeyToken);
                requestedAssembly.Version = targetVersion;
                requestedAssembly.CultureInfo = CultureInfo.InvariantCulture;
                AppDomain.CurrentDomain.AssemblyResolve -= handler;
                return Assembly.Load(requestedAssembly);
            };
            AppDomain.CurrentDomain.AssemblyResolve += handler;
        }
    }
}
