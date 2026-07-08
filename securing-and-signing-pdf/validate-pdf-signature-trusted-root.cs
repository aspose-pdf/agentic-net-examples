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
        const string pdfPath = "signed_document.pdf";   // input PDF with digital signature
        const string caPath  = "trusted_root.cer";      // trusted root CA certificate (DER/PEM)

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(caPath))
        {
            Console.Error.WriteLine($"Root CA certificate not found: {caPath}");
            return;
        }

        // Load the trusted root certificate (using the X509Certificate2 constructor which accepts a file path)
        X509Certificate2 trustedRoot = new X509Certificate2(caPath);

        // Load the PDF document (using the lifecycle rule for disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Prepare validation options – strict mode with chain checking
            var validationOptions = new ValidationOptions
            {
                CheckCertificateChain = true,
                ValidationMode = ValidationMode.Strict
            };

            // Iterate over all fields and process those that are signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField && sigField.Signature != null)
                {
                    // Verify the signature (the trusted root is taken into account by the chain check)
                    bool isValid = sigField.Signature.Verify(validationOptions, out ValidationResult validationResult);

                    // Report the outcome
                    Console.WriteLine($"Signature field: {sigField.PartialName}");
                    Console.WriteLine($"  Valid: {isValid}");

                    // If validation failed, output details (fallback to ToString if specific property is unavailable)
                    if (!isValid && validationResult != null)
                    {
                        Console.WriteLine($"  Validation error: {validationResult}");
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
