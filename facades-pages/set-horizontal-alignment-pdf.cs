using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "aligned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Facade for page‑level operations
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                // Bind the document to the editor
                pageEditor.BindPdf(doc);

                // Specify the page(s) to edit (1‑based indexing)
                pageEditor.ProcessPages = new int[] { 1 };

                // Set horizontal alignment – choose Left, Center, or Right
                pageEditor.HorizontalAlignment = HorizontalAlignment.Center;

                // Save the modified PDF
                pageEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"Alignment applied and saved to '{outputPath}'.");
    }
}
