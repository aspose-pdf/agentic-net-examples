using System;
using System.Collections.Generic;
using System.IO;
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form with fields
            if (doc?.Form?.Fields == null)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Collect only the signature fields from the form
            var signatureFields = new List<SignatureField>();
            foreach (var field in doc.Form.Fields)
            {
                if (field is SignatureField sig)
                    signatureFields.Add(sig);
            }

            if (signatureFields.Count == 0)
            {
                Console.WriteLine("No signature fields found in the document.");
                return;
            }

            // Iterate over each signature field
            foreach (SignatureField sigField in signatureFields)
            {
                Console.WriteLine($"Signature field name: {sigField.PartialName}");

                // Extract the signer's certificate from the signature field
                X509Certificate2 signerCert = sigField.ExtractCertificateObject();
                if (signerCert != null)
                {
                    Console.WriteLine($"  Signer: {signerCert.Subject}");
                    Console.WriteLine($"  Certificate expires on: {signerCert.NotAfter:u}");

                    // Build the certificate chain using X509Chain
                    var chain = new X509Chain();
                    // Optional: configure chain policy if needed (e.g., revocation checking)
                    chain.Build(signerCert);

                    if (chain.ChainElements != null && chain.ChainElements.Count > 0)
                    {
                        Console.WriteLine("  Certificate chain:");
                        foreach (X509ChainElement element in chain.ChainElements)
                        {
                            X509Certificate2 chainCert = element.Certificate;
                            Console.WriteLine($"    Subject: {chainCert.Subject}");
                            Console.WriteLine($"    Expires : {chainCert.NotAfter:u}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  No certificate chain could be built.");
                    }
                }
                else
                {
                    Console.WriteLine("  No certificate could be extracted from the signature.");
                }

                Console.WriteLine(); // Blank line between signatures
            }
        }
    }
}
