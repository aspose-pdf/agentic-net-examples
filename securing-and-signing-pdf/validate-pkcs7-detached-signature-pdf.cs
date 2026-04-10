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
            // Prepare validation options – enable certificate‑chain checking
            ValidationOptions valOptions = new ValidationOptions
            {
                CheckCertificateChain = true,
                ValidationMode = ValidationMode.Strict
            };

            bool anySignatureFound = false;

            // Iterate over all form fields and process those that are signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    anySignatureFound = true;

                    // The Signature object associated with the field
                    // (Signature is the base class for PKCS7, PKCS7Detached, etc.)
                    Signature signature = sigField.Signature;

                    // Verify the signature using the validation options
                    bool isValid = signature.Verify(valOptions, out ValidationResult result);

                    Console.WriteLine($"Signature field: {sigField.PartialName}");
                    Console.WriteLine($"  Valid: {isValid}");

                    // Basic information from the ValidationResult (if needed)
                    // Note: ValidationResult may contain additional details such as error messages.
                    // Here we simply output the result's ToString() for diagnostic purposes.
                    Console.WriteLine($"  Validation result: {result}");
                }
            }

            if (!anySignatureFound)
            {
                Console.WriteLine("No digital signatures were found in the document.");
            }
        }
    }
}