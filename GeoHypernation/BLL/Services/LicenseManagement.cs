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
        private string publicKey;
        public int days = 0;
        //public IEnumerable<IValidationFailure> validationFailures;
        public LicenseManagement()
        {
            if (File.Exists(LicensePath))
            {
                license = License.Load(File.ReadAllText(LicensePath));
                publicKey = "MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEQfR8GuRqevZPbZ1n2x63K9hG7U0hsmz5BkJS14tA72otANWoW/1Ism9FtQbqtwmPgeHoVEAG8wD3KXNU08Qkmg==";

            }
        }
        public bool Check()
        {
            if (license != null)
            {
                if (license.Type == LicenseType.Standard)
                {
                    try
                    {
                        var validationFailures = license.Validate()
                                .ExpirationDate()
                                .When(lic => lic.Type == LicenseType.Standard)
                                .And()
                                .Signature(publicKey)
                                .AssertValidLicense();
                        if (validationFailures.Any())
                        {
                            foreach (var failure in validationFailures)
                            {
                                Console.WriteLine(failure.GetType().Name + ": " + failure.Message + " - " + failure.HowToResolve);
                                return false;
                            }
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    try
                    {
                        TimeSpan dt = license.Expiration - DateTime.Now;
                        days = dt.Days + 1;
                    }
                    catch { }
                    try
                    {
                        var validationFailures = license.Validate()
                                .ExpirationDate()
                                .When(lic => lic.Type == LicenseType.Trial)
                                .And()
                                .Signature(publicKey)
                                .AssertValidLicense();
                        if (validationFailures.Any())
                        {
                            foreach (var failure in validationFailures)
                            {
                                Console.WriteLine(failure.GetType().Name + ": " + failure.Message + " - " + failure.HowToResolve);
                                return false;
                            }
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            else
                return false;
        }
    }
}
