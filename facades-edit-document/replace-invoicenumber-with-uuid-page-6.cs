using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Generate a new UUID to replace the placeholder text
        string uuid = Guid.NewGuid().ToString();

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfContentEditor facade and bind the loaded document
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc);

                // Replace the exact case‑sensitive string "InvoiceNumber" on page 6
                // Page numbers are 1‑based in Aspose.Pdf, so page 6 is passed as 6
                editor.ReplaceText("InvoiceNumber", 6, uuid);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replaced with UUID and saved to '{outputPath}'.");
    }
}