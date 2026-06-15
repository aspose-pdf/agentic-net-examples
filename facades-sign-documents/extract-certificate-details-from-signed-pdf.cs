using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the facade and bind the PDF
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);

            // Retrieve the signature names (IList<SignatureName>)
            IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames(true);
            if (signatureNames == null || signatureNames.Count == 0)
            {
                Console.WriteLine("No digital signatures found in the document.");
                return;
            }

            // Process the first signature (or iterate as needed)
            SignatureName sigInfo = signatureNames[0]; // contains .Name and other metadata

            // Try to extract the certificate directly as X509Certificate2 (preferred API)
            if (pdfSignature.TryExtractCertificate(sigInfo, out X509Certificate2 certificate))
            {
                Console.WriteLine($"Signature Name : {sigInfo.Name}");
                Console.WriteLine($"Issuer         : {certificate.Issuer}");
                Console.WriteLine($"Expiration Date: {certificate.NotAfter}");
            }
            else
            {
                // Fallback to the older ExtractCertificate method if needed
                using (Stream certStream = pdfSignature.ExtractCertificate(sigInfo))
                {
                    if (certStream == null)
                    {
                        Console.WriteLine($"No certificate associated with signature '{sigInfo.Name}'.");
                        return;
                    }

                    using (MemoryStream ms = new MemoryStream())
                    {
                        certStream.CopyTo(ms);
                        byte[] certBytes = ms.ToArray();
                        // Loading via constructor is still functional (warning only)
                        X509Certificate2 fallbackCert = new X509Certificate2(certBytes);
                        Console.WriteLine($"Signature Name : {sigInfo.Name}");
                        Console.WriteLine($"Issuer         : {fallbackCert.Issuer}");
                        Console.WriteLine($"Expiration Date: {fallbackCert.NotAfter}");
                    }
                }
            }
        }
    }
}
