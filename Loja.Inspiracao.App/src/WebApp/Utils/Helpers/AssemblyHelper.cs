using System.Reflection;

namespace WebApp.Utils.Helpers
{
    public static class AssemblyHelper
    {
        public static string GetNameApp()
        {
            string Namespace = Assembly.GetEntryAssembly().GetName().Name;
            return Namespace.Substring(0, Namespace.LastIndexOf("."));
        }
    }
}
