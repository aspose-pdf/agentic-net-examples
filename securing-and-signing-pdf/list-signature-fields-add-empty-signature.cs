using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Disable automatic sanitization so existing signatures remain intact
            doc.EnableSignatureSanitization = false;

            // List all existing signature fields
            Console.WriteLine("Existing signature fields:");
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    Console.WriteLine($"- Name: {sigField.Name}, Page: {sigField.PageIndex}, Rect: {sigField.Rect}");
                }
            }

            // Define the rectangle for the new signature field (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a new empty signature field on the first page
            SignatureField newSignature = new SignatureField(doc, rect)
            {
                Name = "NewSignature",
                AlternateName = "Signature for future signing"
            };

            // Add the new field to the form on page 1 (Aspose.Pdf uses 1‑based page indexing)
            doc.Form.Add(newSignature, 1);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}