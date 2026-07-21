using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document contains a form with fields
            if (doc.Form == null || doc.Form.Fields == null)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Locate the first signature field that actually holds a signature
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

            // Configure validation options to use OCSP only
            var options = new ValidationOptions
            {
                ValidationMethod = ValidationMethod.Ocsp,   // Use OCSP protocol for revocation checking
                ValidationMode = ValidationMode.Strict,     // Strict validation – any failure marks the signature invalid
                CheckCertificateChain = false,             // Skip chain validation, check only revocation status
                RequestTimeout = 5000                      // Network timeout in milliseconds
            };

            // Verify the signature using the configured options
            bool isValid = sigField.Signature.Verify(options, out ValidationResult validationResult);

            // Output the verification result and detailed status
            Console.WriteLine($"Signature field '{sigField.PartialName}' verification returned: {isValid}");
            Console.WriteLine($"Validation status: {validationResult.Status}");
        }
    }
}
