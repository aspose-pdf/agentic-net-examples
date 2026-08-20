using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

class PdfSignatureValidator
{
    static void Main()
    {
        const string pdfPath = "signed_document.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document (using a using‑statement for proper disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Ensure the document contains a form with fields
            if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields found in the PDF.");
                return;
            }

            // Prepare validation options – check the full certificate chain and use strict mode
            var options = new ValidationOptions
            {
                CheckCertificateChain = true,
                ValidationMode = ValidationMode.Strict
            };

            bool foundSignature = false;

            // Iterate over all form fields and process only signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField && sigField.Signature != null)
                {
                    foundSignature = true;

                    // Verify the signature using the prepared options
                    ValidationResult result;
                    bool isValid = sigField.Signature.Verify(options, out result);

                    Console.WriteLine($"Signature field: {sigField.PartialName}");
                    Console.WriteLine($"  Valid: {isValid}");

                    // Report detailed validation information (Status is always available)
                    if (result != null)
                    {
                        Console.WriteLine($"  Validation status: {result.Status}");
                    }

                    Console.WriteLine();
                }
            }

            if (!foundSignature)
            {
                Console.WriteLine("No digital signatures found in the PDF.");
            }
        }
    }
}
