using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

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

        // Facade for working with digital signatures
        using (PdfFileSignature signatureFacade = new PdfFileSignature())
        {
            // Load the PDF into the facade
            signatureFacade.BindPdf(inputPdf);

            // Retrieve all signature names present in the document
            IList<SignatureName> signatureNames = signatureFacade.GetSignatureNames(true);

            if (signatureNames == null || signatureNames.Count == 0)
            {
                Console.WriteLine("No signature fields found in the PDF.");
                return;
            }

            foreach (SignatureName sigName in signatureNames)
            {
                // Extract the certificate associated with the signature
                using (Stream certStream = signatureFacade.ExtractCertificate(sigName))
                {
                    if (certStream != null && certStream.Length > 0)
                    {
                        byte[] certBytes;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            certStream.CopyTo(ms);
                            certBytes = ms.ToArray();
                        }

                        X509Certificate2 cert = new X509Certificate2(certBytes);

                        Console.WriteLine($"Signature Field: {sigName}");
                        Console.WriteLine($"  Issuer          : {cert.Issuer}");
                        Console.WriteLine($"  Subject         : {cert.Subject}");
                        Console.WriteLine($"  Expiration Date : {cert.NotAfter}");
                        Console.WriteLine($"  Valid From      : {cert.NotBefore}");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine($"Signature Field: {sigName} has no associated certificate.");
                    }
                }
            }
        }
    }
}
