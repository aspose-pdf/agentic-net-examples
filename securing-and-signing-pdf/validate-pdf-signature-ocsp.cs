using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string pdfPath = "signed_document.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Prepare validation options to use OCSP and strict mode
            ValidationOptions options = new ValidationOptions
            {
                ValidationMethod = ValidationMethod.Ocsp,   // Use OCSP only
                ValidationMode   = ValidationMode.Strict,   // Fail the signature if validation fails
                CheckCertificateChain = false,             // Skip chain check, only revocation status
                RequestTimeout = 5000                     // 5 seconds timeout for network calls
            };

            // Iterate over all signature fields in the document
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField && sigField.Signature != null)
                {
                    // Verify the signature with the specified options
                    ValidationResult result;
                    bool isValid = sigField.Signature.Verify(options, out result);

                    // Output the verification result
                    Console.WriteLine($"Signature field: {sigField.PartialName}");
                    Console.WriteLine($"  Verified: {isValid}");
                    Console.WriteLine($"  Validation status: {result.Status}");
                    Console.WriteLine();
                }
            }
        }
    }
}
