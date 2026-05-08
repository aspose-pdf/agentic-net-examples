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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPdf))
        {
            // Prepare validation options (strict mode, check certificate chain)
            ValidationOptions options = new ValidationOptions
            {
                CheckCertificateChain = true,
                ValidationMode = ValidationMode.Strict
            };

            // Iterate over all signature fields in the document
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField && sigField.Signature != null)
                {
                    // Verify the signature using the prepared options
                    bool isValid = sigField.Signature.Verify(options, out ValidationResult validationResult);

                    // Output basic information about the signature
                    Console.WriteLine($"Signature field: {sigField.PartialName}");
                    Console.WriteLine($"  Valid: {isValid}");

                    // If validation failed, report details from ValidationResult
                    if (!isValid && validationResult != null)
                    {
                        Console.WriteLine($"  Validation status: {validationResult.Status}");
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
