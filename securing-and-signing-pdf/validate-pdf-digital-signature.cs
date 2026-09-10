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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Locate the first signature field in the document by iterating over Form.Fields
            SignatureField sigField = null;
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField signature)
                {
                    sigField = signature;
                    break;
                }
            }

            if (sigField == null)
            {
                Console.WriteLine("No digital signature found in the PDF.");
                return;
            }

            // Configure validation options: enable certificate‑chain checking and use strict mode
            ValidationOptions validationOptions = new ValidationOptions
            {
                CheckCertificateChain = true,
                ValidationMode = ValidationMode.Strict
            };

            // Verify the signature using the configured options
            bool isValid = sigField.Signature.Verify(validationOptions, out ValidationResult validationResult);

            Console.WriteLine($"Signature verification result: {(isValid ? "Valid" : "Invalid")}");
            // Additional details can be inspected via validationResult if needed
        }
    }
}
