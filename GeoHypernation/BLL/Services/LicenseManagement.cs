using Standard.Licensing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class LicenseManagement
    {
        private readonly static string exeDir = Path.GetDirectoryName((new System.Uri(Assembly.GetEntryAssembly().CodeBase)).AbsolutePath);
        private readonly string LicensePath = Path.Combine(exeDir, "License.lic");
        public License license = null;
        public LicenseManagement()
        {
            if (File.Exists(LicensePath))
            {
                license = License.Load(File.ReadAllText(LicensePath));
            }
        }
    }
}
