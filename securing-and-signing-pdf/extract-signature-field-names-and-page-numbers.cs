using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form with fields
            if (doc.Form?.Fields == null || !doc.Form.Fields.Any())
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            Console.WriteLine("Signature fields and their page numbers:");
            // Iterate over all form fields
            foreach (Field field in doc.Form.Fields)
            {
                // Filter only signature fields
                if (field is SignatureField sigField)
                {
                    // SignatureField exposes PageIndex (0‑based). Convert to 1‑based for display.
                    int pageNumber = sigField.PageIndex + 1;
                    string fieldName = sigField.PartialName; // Name of the signature field
                    Console.WriteLine($"- Name: {fieldName}, Page: {pageNumber}");
                }
            }
        }
    }
}
