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

        // Load the document to obtain the total page count
        using (var doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;
            int[] evenPages = Enumerable.Range(1, pageCount)
                                        .Where(p => p % 2 == 0)
                                        .ToArray();

            // Use PdfPageEditor to set display duration for the selected pages
            using (var editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);
                editor.ProcessPages = evenPages;   // 1‑based page numbers
                editor.DisplayDuration = 5;        // seconds per even‑numbered page
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
