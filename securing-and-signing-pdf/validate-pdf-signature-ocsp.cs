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
        const string inputPath = "signed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains form fields (signatures are stored as fields)
            if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Configure validation options to use OCSP
            var options = new ValidationOptions
            {
                ValidationMethod = ValidationMethod.Ocsp,   // Use OCSP as the validation method
                CheckCertificateChain = false,              // Only revocation status is required
                RequestTimeout = 5000,                      // 5‑second network timeout
                ValidationMode = ValidationMode.Strict      // Strict mode – validation result influences the signature validity
            };

            bool foundSignature = false;

            // Iterate over all form fields and process only signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField && sigField.Signature != null)
                {
                    foundSignature = true;

                    // Perform verification using the OCSP settings
                    bool isValid = sigField.Signature.Verify(options, out ValidationResult validationResult);

                    // Output the verification outcome
                    Console.WriteLine($"Signature field name: {sigField.PartialName}");
                    Console.WriteLine($"Signature valid: {isValid}");
                    Console.WriteLine($"Validation status: {validationResult.Status}");
                    Console.WriteLine(new string('-', 40));
                }
            }

            if (!foundSignature)
            {
                Console.WriteLine("No digital signatures found in the document.");
            }
        }
    }
}
