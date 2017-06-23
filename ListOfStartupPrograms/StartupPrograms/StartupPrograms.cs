using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ListOfStartupPrograms.StartupPrograms
{
    class StartupPrograms
    {
        protected Icon GetImage(string filePath)
        {
            return Icon.ExtractAssociatedIcon(filePath);
        }
        protected void CheckDigitalSignature(ProgramDTO program)
        {
            if (!File.Exists(program.FilePath))
            {
                Debug.WriteLine("File not found");
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
                Debug.WriteLine("No digital signature found: " + ex.Message);
                program.CompanyName = CompanyNameFromFileInfo(program.FilePath);
                return;
            }
            
            var theCertificateChain = new X509Chain();

            theCertificateChain.ChainPolicy.RevocationFlag = X509RevocationFlag.ExcludeRoot;
            
            theCertificateChain.ChainPolicy.RevocationMode = X509RevocationMode.Offline;

            theCertificateChain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);

            theCertificateChain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;

            program.IsDigitalSignatureCorrect = theCertificateChain.Build(theCertificate);
            if (program.IsDigitalSignatureCorrect)
            {
                Debug.WriteLine("Publisher Information : " + theCertificate.SubjectName.Name);
                Debug.WriteLine("Valid From: " + theCertificate.GetEffectiveDateString());
                Debug.WriteLine("Valid To: " + theCertificate.GetExpirationDateString());
                Debug.WriteLine("Issued By: " + theCertificate.Issuer);
            }
            else
            {
                Debug.WriteLine("Chain Not Valid (certificate is self-signed)");
            }
        }

        private static string CompanyNameFromFileInfo(string path)
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(path);
            return versionInfo.CompanyName;
        }
        private static List<string> Parse(string data, string delimiter)
        {
            if (data == null) return null;
            if (!delimiter.EndsWith("=")) delimiter = delimiter + "=";
            if (!data.Contains(delimiter)) return null;
            var result = new List<string>();
            int start = data.IndexOf(delimiter) + delimiter.Length;
            int length = data.IndexOf(',', start) - start;
            if (length == 0) return null;
            if (length > 0)
            {
                result.Add(data.Substring(start, length));
                var rec = Parse(data.Substring(start + length), delimiter);
                if (rec != null) result.AddRange(rec);
            }
            else
            {
                result.Add(data.Substring(start));
            }
            return result;
        }
    }
}
