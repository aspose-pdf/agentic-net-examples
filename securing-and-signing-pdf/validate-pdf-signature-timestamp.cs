using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

class ValidatePdfSignature
{
    static void Main()
    {
        const string inputPath = "signed_document.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the standard Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all fields and filter for signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // Prepare validation options (strict mode, check certificate chain)
                    ValidationOptions options = new ValidationOptions
                    {
                        ValidationMode = ValidationMode.Strict,
                        CheckCertificateChain = true
                    };

                    // Perform verification and obtain detailed validation result
                    ValidationResult validationResult;
                    bool isSignatureValid = sigField.Signature.Verify(options, out validationResult);

                    // Retrieve the signing timestamp from the signature
                    DateTime signingTime = sigField.Signature.Date;

                    // Extract the signing certificate from the signature field
                    X509Certificate2 signingCert = sigField.ExtractCertificateObject();

                    bool timestampWithinCert = false;
                    if (signingCert != null)
                    {
                        // Check that the signing timestamp falls within the certificate's validity period
                        timestampWithinCert = signingTime >= signingCert.NotBefore && signingTime <= signingCert.NotAfter;
                    }

                    // Output the verification outcome
                    Console.WriteLine($"Signature '{sigField.PartialName}':");
                    Console.WriteLine($"  Valid signature          : {isSignatureValid}");
                    Console.WriteLine($"  Signing time            : {signingTime:u}");
                    Console.WriteLine($"  Certificate valid period: {signingCert?.NotBefore:u} - {signingCert?.NotAfter:u}");
                    Console.WriteLine($"  Timestamp within period : {timestampWithinCert}");
                    Console.WriteLine();
                }
            }
        }
    }
}
