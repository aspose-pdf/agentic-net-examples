using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains at least one signature field
            bool hasSignature = false;
            if (doc.Form != null && doc.Form.Fields != null)
            {
                foreach (Field f in doc.Form.Fields)
                {
                    if (f is SignatureField)
                    {
                        hasSignature = true;
                        break;
                    }
                }
            }

            if (!hasSignature)
            {
                Console.WriteLine("No digital signatures found in the document.");
                return;
            }

            // Iterate over each signature field
            foreach (Field field in doc.Form.Fields)
            {
                if (field is not SignatureField sigField)
                    continue;

                Console.WriteLine($"Signature field: {sigField.PartialName}");

                // Extract the X509 certificate object from the signature field
                X509Certificate2 cert = sigField.ExtractCertificateObject();
                if (cert == null)
                {
                    Console.WriteLine("  No certificate attached to this signature.");
                    continue;
                }

                // Build the certificate chain using .NET X509Chain
                X509Chain chain = new X509Chain();
                // Optional: you can configure chain.ChainPolicy if needed
                bool chainBuilt = chain.Build(cert);

                if (!chainBuilt)
                {
                    Console.WriteLine("  Unable to build certificate chain.");
                }

                // Collect all certificates in the chain (including the leaf)
                List<X509Certificate2> chainCertificates = new List<X509Certificate2>();
                foreach (X509ChainElement element in chain.ChainElements)
                {
                    chainCertificates.Add(element.Certificate);
                }

                // Verify expiration date for each certificate in the chain
                foreach (X509Certificate2 chainCert in chainCertificates)
                {
                    DateTime notAfter = chainCert.NotAfter;
                    bool isExpired = DateTime.UtcNow > notAfter.ToUniversalTime();

                    Console.WriteLine($"  Subject: {chainCert.Subject}");
                    Console.WriteLine($"    Expires on: {notAfter:u}");
                    Console.WriteLine($"    Expired: {(isExpired ? "Yes" : "No")}");
                }

                Console.WriteLine();
            }
        }
    }
}
