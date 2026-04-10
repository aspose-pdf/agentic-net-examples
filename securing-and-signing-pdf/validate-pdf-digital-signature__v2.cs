using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

class PdfSignatureValidator
{
    static void Main()
    {
        const string inputPdfPath = "signed_document.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Prepare validation options: strict mode with certificate‑chain checking
            ValidationOptions options = new ValidationOptions
            {
                ValidationMode = ValidationMode.Strict,
                CheckCertificateChain = true
            };

            bool anySignatureFound = false;

            // Iterate over all fields and process only signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField && sigField.Signature != null)
                {
                    anySignatureFound = true;
                    Signature signature = sigField.Signature;

                    // Verify the signature using the prepared options
                    bool isValid = signature.Verify(options, out ValidationResult validationResult);

                    Console.WriteLine($"Signature field: {sigField.PartialName}");
                    Console.WriteLine($"  Valid: {isValid}");

                    // Report detailed validation information if available
                    if (validationResult != null)
                    {
                        Console.WriteLine($"  Validation status: {validationResult.Status}");
                        // ValidationResult does not expose an ErrorMessage property in the current API version.
                        // If additional error details are required, they can be obtained via other means such as
                        // logging or catching exceptions during verification.
                    }

                    Console.WriteLine();
                }
            }

            if (!anySignatureFound)
            {
                Console.WriteLine("No digital signatures were found in the document.");
            }
        }
    }
}
