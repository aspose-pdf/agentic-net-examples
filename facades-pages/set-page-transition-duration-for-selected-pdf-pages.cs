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

        // Define the page range (example: pages 2 through 5)
        int startPage = 2;
        int endPage = 5;

        // Load the PDF into the Facades editor
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Specify the pages that will receive the transition (1‑based indices)
            editor.ProcessPages = Enumerable.Range(startPage, endPage - startPage + 1).ToArray();

            // Set transition type (Fade). TransitionType is an *int* property – use the integer value that
            // represents the desired transition. In Aspose.PDF 26.x the value for Fade is 4.
            editor.TransitionType = 4; // Fade transition

            // Set transition duration in seconds (1 second)
            editor.TransitionDuration = 1;

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Pages {startPage}-{endPage} now have a 1‑second fade transition. Saved to '{outputPath}'.");
    }
}
