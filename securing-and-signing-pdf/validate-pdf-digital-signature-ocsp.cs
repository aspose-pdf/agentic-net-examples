using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Locate the first signature field that actually contains a signature
            SignatureField sigField = null;
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField s && s.Signature != null)
                {
                    sigField = s;
                    break;
                }
            }

            if (sigField == null)
            {
                Console.WriteLine("No digital signatures found in the document.");
                return;
            }

            // Configure validation to use OCSP and strict mode
            ValidationOptions options = new ValidationOptions
            {
                ValidationMethod = ValidationMethod.Ocsp,   // Use OCSP for revocation checking
                CheckCertificateChain = false,              // Skip full chain check, focus on revocation
                ValidationMode = ValidationMode.Strict,      // Fail the signature if validation fails
                RequestTimeout = 10000                      // 10 seconds timeout for network calls
            };

            // Perform the verification
            ValidationResult validationResult;
            bool isSignatureValid = sigField.Signature.Verify(options, out validationResult);

            // Output the results
            Console.WriteLine($"Signature verification succeeded: {isSignatureValid}");
            Console.WriteLine($"Certificate validation status: {validationResult.Status}");
        }
    }
}
