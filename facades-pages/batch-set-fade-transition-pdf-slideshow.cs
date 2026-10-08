using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // source 200‑page PDF
        const string outputPath = "output.pdf"; // PDF with slide transitions

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Use PdfPageEditor to apply a Fade transition (2‑second duration) to every page
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(doc);

            // Specify the pages to edit – 1‑based indexing
            editor.ProcessPages = Enumerable.Range(1, doc.Pages.Count).ToArray();

            // TransitionType uses the integer value defined by Aspose.Pdf's TransitionType enum.
            // Fade corresponds to the value 11 in the current library version.
            editor.TransitionType = 11;          // Fade transition
            editor.TransitionDuration = 2;       // 2 seconds

            // Persist the changes
            editor.Save(outputPath);
        }

        Console.WriteLine($"Transitions applied and saved to '{outputPath}'.");
    }
}
