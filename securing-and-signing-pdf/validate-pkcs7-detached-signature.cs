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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Find the first signature field in the document
            SignatureField sigField = null;
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField s)
                {
                    sigField = s;
                    break;
                }
            }

            if (sigField == null)
            {
                Console.WriteLine("No signature fields found in the document.");
                return;
            }

            // Retrieve the underlying Signature object (PKCS7Detached, PKCS7, etc.)
            Signature signature = sigField.Signature;

            // Prepare validation options – enable certificate‑chain checking
            ValidationOptions validationOptions = new ValidationOptions
            {
                CheckCertificateChain = true,
                // Use Strict mode so that a failed chain makes the signature invalid
                ValidationMode = ValidationMode.Strict
            };

            // Perform verification and obtain detailed validation result
            ValidationResult validationResult;
            bool isSignatureValid = signature.Verify(validationOptions, out validationResult);

            // Output the verification outcome
            Console.WriteLine($"Signature valid: {isSignatureValid}");
            Console.WriteLine($"Certificate chain checked: {validationOptions.CheckCertificateChain}");
            // The ValidationResult may contain additional details (e.g., revocation status)
            // Here we simply display its string representation
            Console.WriteLine($"Validation result: {validationResult}");
        }
    }
}
