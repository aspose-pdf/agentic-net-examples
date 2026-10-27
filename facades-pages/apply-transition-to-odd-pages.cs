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

        // Build an array of odd‑numbered page indexes (1‑based)
        int[] oddPages = Enumerable.Range(1, pageCount)
                                   .Where(p => p % 2 == 1)
                                   .ToArray();

        // Apply a Fade transition (value 3) of 1 second to the odd pages
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = oddPages;   // target pages
            editor.TransitionType = 3;        // Fade transition (int representation)
            editor.TransitionDuration = 1;    // duration in seconds
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF with alternating transitions saved to '{outputPath}'.");
    }
}
