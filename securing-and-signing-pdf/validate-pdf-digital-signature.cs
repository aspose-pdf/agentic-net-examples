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
        const string pdfPath = "signed.pdf";
        const string logPath = "signature_validation_log.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        using (Document doc = new Document(pdfPath))
        {
            // Use LINQ Any() because FieldCollection does not expose a Count property.
            if (doc.Form == null || doc.Form.Fields == null || !doc.Form.Fields.Any())
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            bool anySignature = false;
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField && sigField.Signature != null)
                {
                    anySignature = true;
                    var options = new ValidationOptions
                    {
                        CheckCertificateChain = true,
                        ValidationMode = ValidationMode.Strict
                    };

                    ValidationResult validationResult;
                    bool isValid = sigField.Signature.Verify(options, out validationResult);

                    Console.WriteLine($"Signature field: {sigField.PartialName}");
                    Console.WriteLine($"  Overall validity (Verify return): {isValid}");
                    Console.WriteLine($"  Validation result: {validationResult}");
                }
            }

            if (!anySignature)
            {
                Console.WriteLine("No digital signatures found in the document.");
            }

            // Write structural validation log (PDF/A‑1B)
            doc.Validate(logPath, PdfFormat.PDF_A_1B);
        }

        Console.WriteLine("Signature validation completed.");
    }
}
