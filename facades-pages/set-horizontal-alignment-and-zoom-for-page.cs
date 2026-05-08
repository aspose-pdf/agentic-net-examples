using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade and bind it to the loaded document
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Set horizontal alignment to Center for better readability
            editor.HorizontalAlignment = HorizontalAlignment.Center;

            // Set zoom coefficient to 1.2 (120%)
            editor.Zoom = 1.2f;

            // Apply the changes only to page 4 (1‑based indexing)
            editor.ProcessPages = new int[] { 4 };

            // Apply the configured changes to the document
            editor.ApplyChanges();

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}