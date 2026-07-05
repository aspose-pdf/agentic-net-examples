using System;
using System.IO;
using System.Linq; // Needed for Count() extension method
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

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form with fields
            if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields (including signature fields) found in the document.");
                return;
            }

            // Configure validation to use CRL distribution points and strict mode
            var validationOptions = new ValidationOptions
            {
                // Use only CRL for revocation checking
                ValidationMethod = ValidationMethod.Crl,
                // Strict mode makes revocation failures cause overall signature invalidation
                ValidationMode = ValidationMode.Strict,
                // Do not check the full certificate chain (only revocation status is needed)
                CheckCertificateChain = false
            };

            // Iterate over each field, filter for signature fields, and verify them
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField && sigField.Signature != null)
                {
                    ValidationResult validationResult;
                    bool isValid = sigField.Signature.Verify(validationOptions, out validationResult);

                    Console.WriteLine($"Signature field '{sigField.PartialName}': Valid = {isValid}");
                    // Optional: display additional information from validationResult if required
                }
            }
        }
    }
}
