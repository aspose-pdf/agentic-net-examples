using System;
using System.IO;
using System.Linq;
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

        // Load the document to obtain the page count.
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;
            // Build an array of 1‑based even page numbers.
            int[] evenPages = Enumerable.Range(1, pageCount)
                                        .Where(p => p % 2 == 0)
                                        .ToArray();

            var editor = new PdfPageEditor();
            editor.BindPdf(inputPath);
            // Specify which pages to process.
            editor.ProcessPages = evenPages;
            // Apply a zoom factor of 1.2 (120%).
            editor.Zoom = 1.2f;
            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Even pages zoomed and saved to '{outputPath}'.");
    }
}
