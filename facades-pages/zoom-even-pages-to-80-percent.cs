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
        Document doc = new Document(inputPath);
        int pageCount = doc.Pages.Count;

        // Build an array of even‑numbered page indices (1‑based as required by Aspose)
        int[] evenPages = Enumerable.Range(1, pageCount)
                                    .Where(p => p % 2 == 0)
                                    .ToArray();

        // Apply a zoom factor of 0.8 (80%) to the selected pages using PdfPageEditor
        using (var editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = evenPages; // pages to affect
            editor.Zoom = 0.8f;               // 80% zoom
            editor.Save(outputPath);
        }

        Console.WriteLine($"Even pages zoomed to 0.8 and saved as '{outputPath}'.");
    }
}
