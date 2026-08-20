using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "signed_document.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the recommended lifecycle pattern)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all fields and process only signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // Extract the X509 certificate object from the signature field
                    X509Certificate2 cert = sigField.ExtractCertificateObject();

                    if (cert == null)
                    {
                        Console.WriteLine($"Signature field '{sigField.PartialName}' does not contain a certificate.");
                        continue;
                    }

                    Console.WriteLine($"Signature field: {sigField.PartialName}");
                    Console.WriteLine($"  Subject: {cert.Subject}");
                    Console.WriteLine($"  Issuer : {cert.Issuer}");
                    Console.WriteLine($"  Valid From: {cert.NotBefore}");
                    Console.WriteLine($"  Valid To  : {cert.NotAfter}");
                    Console.WriteLine($"  Expired?  : {(DateTime.UtcNow > cert.NotAfter ? "Yes" : "No")}");

                    // Build the certificate chain for this certificate
                    using (X509Chain chain = new X509Chain())
                    {
                        // Use default chain policy; you can customize if needed
                        chain.Build(cert);

                        Console.WriteLine($"  Chain elements count: {chain.ChainElements.Count}");

                        for (int i = 0; i < chain.ChainElements.Count; i++)
                        {
                            X509Certificate2 chainCert = chain.ChainElements[i].Certificate;
                            Console.WriteLine($"    Chain [{i}] Subject: {chainCert.Subject}");
                            Console.WriteLine($"    Chain [{i}] Issuer : {chainCert.Issuer}");
                            Console.WriteLine($"    Chain [{i}] Valid To: {chainCert.NotAfter}");
                            Console.WriteLine($"    Chain [{i}] Expired? : {(DateTime.UtcNow > chainCert.NotAfter ? "Yes" : "No")}");
                        }
                    }

                    Console.WriteLine(); // blank line between signatures
                }
            }
        }
    }
}
