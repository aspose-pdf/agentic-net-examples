using System;
using System.IO;
using System.Linq;
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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document contains an AcroForm with fields
            if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Configure validation options to use CRL distribution points
            ValidationOptions options = new ValidationOptions
            {
                ValidationMethod = ValidationMethod.Crl,          // Use CRL for revocation checking
                ValidationMode   = ValidationMode.Strict,        // Fail the signature if validation fails
                CheckCertificateChain = false                  // Only check revocation status, not the full chain
            };

            // Iterate over each field and process signature fields only
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField && sigField.Signature != null)
                {
                    // Perform verification with the specified options
                    bool isValid = sigField.Signature.Verify(options, out ValidationResult validationResult);

                    // Output verification result
                    Console.WriteLine($"Signature field '{sigField.PartialName}': {(isValid ? "Valid" : "Invalid")}");
                    Console.WriteLine($"  Validation result: {validationResult.Status}");
                }
            }
        }
    }
}
