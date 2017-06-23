using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ListOfStartupPrograms.StartupPrograms
{
    class StartupPrograms
    {
        protected void CheckDigitalSignature(ProgramDTO program)
        {
            if (!File.Exists(program.FilePath))
            {
                Console.WriteLine("File not found");
                return;
            }

            X509Certificate2 theCertificate;

            try
            {
                X509Certificate theSigner = X509Certificate.CreateFromSignedFile(program.FilePath);
                theCertificate = new X509Certificate2(theSigner);
                program.IsDigitalSignatureExists = true;
                program.CompanyName = Parse(theCertificate.Subject, "CN").FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("No digital signature found: " + ex.Message);
                program.IsDigitalSignatureExists = false;
                program.IsDigitalSignatureCorrect = false;
                program.CompanyName = CompanyNameFromFileInfo(program.FilePath);
                return;
            }

            bool chainIsValid = false;

            /*
              *
              * This section will check that the certificate is from a trusted authority IE
              * not self-signed.
              *
              */

            var theCertificateChain = new X509Chain();

            theCertificateChain.ChainPolicy.RevocationFlag = X509RevocationFlag.ExcludeRoot;

            /*
              *
              * Using .Online here means that the validation WILL CALL OUT TO THE INTERNET
              * to check the revocation status of the certificate. Change to .Offline if you
              * don't want that to happen.
              */

            theCertificateChain.ChainPolicy.RevocationMode = X509RevocationMode.Offline;

            theCertificateChain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);

            theCertificateChain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;

            chainIsValid = theCertificateChain.Build(theCertificate);
            if (chainIsValid)
            {
                program.IsDigitalSignatureCorrect = true;
                Debug.WriteLine("Publisher Information : " + theCertificate.SubjectName.Name);
                Debug.WriteLine("Valid From: " + theCertificate.GetEffectiveDateString());
                Debug.WriteLine("Valid To: " + theCertificate.GetExpirationDateString());
                Debug.WriteLine("Issued By: " + theCertificate.Issuer);
            }
            else
            {
                program.IsDigitalSignatureCorrect = false;
                Debug.WriteLine("Chain Not Valid (certificate is self-signed)");
            }
        }

        private string CompanyNameFromFileInfo(string path)
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(path);
            return versionInfo.CompanyName;
        }
        public static List<string> Parse(string data, string delimiter)
        {
            if (data == null) return null;
            if (!delimiter.EndsWith("=")) delimiter = delimiter + "=";
            if (!data.Contains(delimiter)) return null;
            //base case
            var result = new List<string>();
            int start = data.IndexOf(delimiter) + delimiter.Length;
            int length = data.IndexOf(',', start) - start;
            if (length == 0) return null; //the group is empty
            if (length > 0)
            {
                result.Add(data.Substring(start, length));
                //only need to recurse when the comma was found, because there could be more groups
                var rec = Parse(data.Substring(start + length), delimiter);
                if (rec != null) result.AddRange(rec); //can't pass null into AddRange() :(
            }
            else //no comma found after current group so just use the whole remaining string
            {
                result.Add(data.Substring(start));
            }
            return result;
        }
    }
}
