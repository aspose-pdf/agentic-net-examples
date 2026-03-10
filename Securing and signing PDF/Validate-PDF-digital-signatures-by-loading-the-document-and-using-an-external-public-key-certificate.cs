using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string pdfPath = "signed_document.pdf";
        const string publicCertPath = "public_certificate.cer";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(publicCertPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {publicCertPath}");
            return;
        }

        // Load the external public key certificate using the recommended loader (avoids obsolete constructor)
        byte[] certBytes = File.ReadAllBytes(publicCertPath);
        X509Certificate2 publicCertificate = X509CertificateLoader.LoadCertificate(certBytes);

        // Configure validation options (strict mode)
        ValidationOptions validationOptions = new ValidationOptions
        {
            ValidationMode = ValidationMode.Strict,
            CheckCertificateChain = true
        };

        // Open the PDF document (disposed automatically)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Ensure the document contains signature fields
            var signatureFields = pdfDocument.Form?.Fields?.OfType<SignatureField>();
            if (signatureFields == null || !signatureFields.Any())
            {
                Console.WriteLine("No digital signatures found in the document.");
                return;
            }

            // Iterate over each signature field and verify
            foreach (SignatureField sigField in signatureFields)
            {
                // The Signature object associated with the field
                Signature signature = sigField.Signature;

                // Verify using the external public key certificate
                bool isValid = signature.Verify(publicCertificate, validationOptions, out ValidationResult validationResult);

                Console.WriteLine($"Signature field '{sigField.Name}': {(isValid ? "Valid" : "Invalid")}");
                Console.WriteLine($"  Validation result: {validationResult?.Status}");
                Console.WriteLine($"  Reason: {validationResult?.Message}");
            }
        }
    }
}
