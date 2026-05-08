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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all form fields and process only signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // Retrieve the underlying signature object (PKCS7, PKCS1, etc.)
                    Signature signature = sigField.Signature;
                    if (signature == null)
                    {
                        Console.WriteLine($"Signature field '{sigField.PartialName}' does not contain a signature.");
                        continue;
                    }

                    // Configure validation options to use CRL distribution points
                    ValidationOptions options = new ValidationOptions
                    {
                        ValidationMethod = ValidationMethod.Crl,          // Use CRL for revocation checking
                        ValidationMode   = ValidationMode.Strict,        // Fail the verification if revocation check fails
                        CheckCertificateChain = false                    // Skip chain validation (focus on revocation)
                    };

                    // Perform the verification
                    bool isValid = signature.Verify(options, out ValidationResult validationResult);

                    // Output the verification result
                    Console.WriteLine($"Signature field '{sigField.PartialName}':");
                    Console.WriteLine($"  Valid: {isValid}");
                    if (validationResult != null)
                    {
                        // The ValidationResult type provides details about the verification outcome.
                        // Typical properties include IsValid, ErrorMessage, etc.
                        // Adjust the property names according to the actual library version if needed.
                        Console.WriteLine($"  Validation details: {validationResult}");
                    }
                }
            }
        }
    }
}