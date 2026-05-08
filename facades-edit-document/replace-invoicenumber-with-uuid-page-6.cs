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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfContentEditor and bind it to the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Generate a new UUID string
            string uuid = Guid.NewGuid().ToString();

            // Replace the exact case‑sensitive text "InvoiceNumber" on page 6
            // Page numbers are 1‑based, so 6 refers to the sixth page.
            editor.ReplaceText("InvoiceNumber", 6, uuid);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replaced with UUID and saved to '{outputPath}'.");
    }
}