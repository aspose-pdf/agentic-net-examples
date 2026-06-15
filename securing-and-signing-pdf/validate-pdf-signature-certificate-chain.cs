using System;
using System.IO;
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            bool foundSignature = false;

            // Iterate over all fields and process only the signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    foundSignature = true;

                    // The actual signature object (PKCS7Detached, PKCS7, PKCS1, etc.)
                    Signature signature = sigField.Signature;

                    // Set up validation options to check the certificate chain
                    ValidationOptions options = new ValidationOptions
                    {
                        CheckCertificateChain = true,          // Enable chain validation
                        ValidationMode = ValidationMode.Strict // Fail verification if chain is invalid
                    };

                    // Verify the signature using the options
                    ValidationResult validationResult;
                    bool isValid = signature.Verify(options, out validationResult);

                    Console.WriteLine($"Signature field '{sigField.PartialName}':");
                    Console.WriteLine($"  Valid: {isValid}");
                    Console.WriteLine($"  Validation result: {validationResult}");
                }
            }

            if (!foundSignature)
            {
                Console.WriteLine("No signature fields were found in the PDF.");
            }
        }
    }
}
