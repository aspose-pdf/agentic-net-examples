using System;
using System.IO;
using System.Linq; // Added for Count() extension method
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string pdfPath = "signed.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document containing digital signatures
        using (Document doc = new Document(pdfPath))
        {
            // Ensure the document has a form and at least one field
            if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Iterate over each field and process only signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField && sigField.Signature != null)
                {
                    // Configure validation options to use OCSP
                    var options = new ValidationOptions
                    {
                        ValidationMethod = ValidationMethod.Ocsp,   // use OCSP
                        ValidationMode = ValidationMode.Strict,     // strict mode
                        CheckCertificateChain = false,             // only revocation check
                        RequestTimeout = 5000                      // 5 seconds timeout
                    };

                    // Perform the verification
                    ValidationResult validationResult;
                    bool isValid = sigField.Signature.Verify(options, out validationResult);

                    // Output verification details
                    Console.WriteLine($"Signature field: {sigField.PartialName}");
                    Console.WriteLine($"Verification passed: {isValid}");
                    Console.WriteLine($"Validation status: {validationResult.Status}");
                    Console.WriteLine();
                }
            }
        }
    }
}
