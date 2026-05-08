using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ValidateSignature
{
    static void Main()
    {
        const string pdfPath = "signed_document.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // -----------------------------------------------------------------
            // 1. Enumerate signature fields present in the document
            // -----------------------------------------------------------------
            bool anySignature = false;
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    anySignature = true;
                    Console.WriteLine();
                    Console.WriteLine($"Signature field name : {sigField.PartialName}");

                    // The Signature object attached to the field (may be null if the field is empty)
                    Signature signature = sigField.Signature;
                    if (signature != null)
                    {
                        // Basic information that the core API exposes
                        Console.WriteLine($"Authority            : {signature.Authority}");
                        Console.WriteLine($"Location             : {signature.Location}");
                        Console.WriteLine($"Reason               : {signature.Reason}");

                        // -----------------------------------------------------------------
                        // 2. Retrieve the signing certificate (if present)
                        // -----------------------------------------------------------------
                        // NOTE: The core Aspose.Pdf API does not expose the certificate data directly.
                        // Access to the raw certificate is only available via the Facades layer (PdfFileSignature),
                        // which is prohibited by the task constraints. Therefore we only display the information
                        // that is accessible through the core API.
                        Console.WriteLine("Certificate information is not available via the core API.");

                        // NOTE: The core Aspose.Pdf API does not expose a direct "Validate" method.
                        // Validation (including chain checking) is provided via the Facades layer (PdfFileSignature),
                        // which is prohibited by the task constraints. Therefore we only display the information
                        // that is accessible through the core API.
                    }
                    else
                    {
                        Console.WriteLine("The signature field is empty (no signature applied).");
                    }
                }
            }

            if (!anySignature)
            {
                Console.WriteLine("No signature fields were found in the document.");
            }
        }
    }
}
