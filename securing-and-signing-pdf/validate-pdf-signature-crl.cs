using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;
using System.Security.Cryptography.X509Certificates;

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
            // Iterate over all form fields and find signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // Obtain the Signature object associated with the field
                    Signature signature = sigField.Signature;

                    // Set up validation options to use CRL checking only
                    ValidationOptions options = new ValidationOptions
                    {
                        ValidationMethod = ValidationMethod.Crl,          // Use CRL distribution points
                        ValidationMode   = ValidationMode.Strict,        // Fail if revocation check fails
                        CheckCertificateChain = false                    // Skip chain validation (focus on revocation)
                    };

                    // Perform verification
                    ValidationResult validationResult;
                    bool isValid = signature.Verify(options, out validationResult);

                    // Output results
                    Console.WriteLine($"Signature field: {sigField.PartialName}");
                    Console.WriteLine($"  Verification passed: {isValid}");
                    Console.WriteLine($"  ValidationResult: {validationResult}");
                }
            }
        }
    }
}