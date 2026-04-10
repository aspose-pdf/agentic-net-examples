using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

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

        // Load the PDF document
        using (Document pdf = new Document(inputPath))
        {
            // Iterate over all fields and process only signature fields
            foreach (Field field in pdf.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // Extract the X.509 certificate object from the signature field
                    X509Certificate2 cert = sigField.ExtractCertificateObject();

                    if (cert == null)
                    {
                        Console.WriteLine($"Signature field '{sigField.PartialName}' does not contain a certificate.");
                        continue;
                    }

                    Console.WriteLine($"Signature field: {sigField.PartialName}");
                    Console.WriteLine($"  Subject: {cert.Subject}");
                    Console.WriteLine($"  Expiration: {cert.NotAfter}");
                    Console.WriteLine($"  Expired: {DateTime.Now > cert.NotAfter}");

                    // Build and examine the certificate chain
                    X509Chain chain = new X509Chain();
                    // Enable chain building with revocation checking if desired
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;

                    bool chainBuilt = chain.Build(cert);
                    if (!chainBuilt)
                    {
                        Console.WriteLine("  Unable to build a complete certificate chain.");
                    }

                    // Iterate through each certificate in the chain
                    for (int i = 0; i < chain.ChainElements.Count; i++)
                    {
                        X509Certificate2 chainCert = chain.ChainElements[i].Certificate;
                        Console.WriteLine($"  Chain element {i + 1}:");
                        Console.WriteLine($"    Subject: {chainCert.Subject}");
                        Console.WriteLine($"    Issuer : {chainCert.Issuer}");
                        Console.WriteLine($"    Expiration: {chainCert.NotAfter}");
                        Console.WriteLine($"    Expired: {DateTime.Now > chainCert.NotAfter}");
                    }

                    // Optional: verify the signature with chain checking enabled
                    ValidationOptions validationOptions = new ValidationOptions
                    {
                        CheckCertificateChain = true,
                        ValidationMode = ValidationMode.Strict
                    };
                    bool isValid = sigField.Signature.Verify(validationOptions, out var validationResult);
                    Console.WriteLine($"  Signature verification result: {isValid}");
                }
            }
        }
    }
}
