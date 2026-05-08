using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Collect all signature fields in the document
            List<SignatureField> signatureFields = new List<SignatureField>();
            if (doc.Form != null && doc.Form.Fields != null)
            {
                foreach (var field in doc.Form.Fields)
                {
                    if (field is SignatureField sigField)
                        signatureFields.Add(sigField);
                }
            }

            if (signatureFields.Count == 0)
            {
                Console.WriteLine("No digital signatures found in the document.");
                return;
            }

            // Process each signature field
            foreach (var sigField in signatureFields)
            {
                Console.WriteLine($"Processing signature field: {sigField.PartialName}");

                // Extract the signing certificate (X509Certificate2)
                X509Certificate2 signingCert = sigField.ExtractCertificateObject();
                if (signingCert == null)
                {
                    Console.WriteLine("  No certificate attached to this signature.");
                    continue;
                }

                // Build the certificate chain
                using (X509Chain chain = new X509Chain())
                {
                    // Optional: configure chain policy if needed
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;

                    bool chainBuilt = chain.Build(signingCert);
                    if (!chainBuilt)
                    {
                        Console.WriteLine("  Unable to build a complete certificate chain.");
                    }

                    // Iterate over each element in the chain
                    for (int i = 0; i < chain.ChainElements.Count; i++)
                    {
                        X509Certificate2 cert = chain.ChainElements[i].Certificate;
                        string subject = cert.Subject;
                        DateTime notAfter = cert.NotAfter;
                        bool isExpired = DateTime.UtcNow > notAfter;

                        Console.WriteLine($"  Chain element {i + 1}:");
                        Console.WriteLine($"    Subject      : {subject}");
                        Console.WriteLine($"    Expiration   : {notAfter:u}");
                        Console.WriteLine($"    Is expired?  : {isExpired}");
                    }
                }
            }
        }
    }
}