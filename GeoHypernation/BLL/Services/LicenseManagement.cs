using Standard.Licensing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Standard.Licensing.Validation;

namespace BLL.Services
{
    public class LicenseManagement
    {
        private readonly static string exeDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        private readonly string LicensePath = Path.Combine(exeDir, "License.lic");
        public License license = null;
        string publicKey;
        public LicenseManagement()
        {
            if (File.Exists(LicensePath))
            {
                license = License.Load(File.ReadAllText(LicensePath));
                var validationFailures = license.Validate()
                    .ExpirationDate()
                    .When(lic => lic.Type == LicenseType.Standard)
                    .And()
                    .Signature(publicKey)
                    .AssertValidLicense();
            }
        }
    }
}
