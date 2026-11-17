using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve all signature fields via the generic Fields collection
            var signatureFields = doc.Form?.Fields?.OfType<SignatureField>()?.ToList();

            if (signatureFields == null || signatureFields.Count == 0)
            {
                Console.WriteLine("No digital signatures found in the document.");
                return;
            }

            // Iterate over each signature field
            foreach (SignatureField sigField in signatureFields)
            {
                // The core API does not expose a direct ValidateSignature method.
                // A simple integrity check is to see whether a signature object exists.
                bool isSigned = sigField.Signature != null;

                Console.WriteLine($"Signature Name: {sigField.PartialName}");
                Console.WriteLine($"Signed (signature object present): {isSigned}");

                // Attempt to extract the signing certificate, if any
                X509Certificate2 cert = null;
                try
                {
                    using (Stream certStream = sigField.ExtractCertificate())
                    {
                        if (certStream != null && certStream.Length > 0)
                        {
                            // Load the certificate from the extracted stream
                            using (var ms = new MemoryStream())
                            {
                                certStream.CopyTo(ms);
                                cert = new X509Certificate2(ms.ToArray());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error extracting certificate: {ex.Message}");
                }

                if (cert != null)
                {
                    Console.WriteLine("Certificate Details:");
                    Console.WriteLine($"  Subject: {cert.Subject}");
                    Console.WriteLine($"  Issuer: {cert.Issuer}");
                    Console.WriteLine($"  Valid From: {cert.NotBefore}");
                    Console.WriteLine($"  Valid To: {cert.NotAfter}");
                    Console.WriteLine($"  Thumbprint: {cert.Thumbprint}");
                    Console.WriteLine($"  Serial Number: {cert.SerialNumber}");
                }
                else
                {
                    Console.WriteLine("  No certificate information available.");
                }

                Console.WriteLine(new string('-', 40));
            }
        }
    }
}
