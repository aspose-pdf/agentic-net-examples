using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_zoomed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the document only to obtain the total page count.
            using (Document doc = new Document(inputPath))
            {
                // Prepare an array with all page numbers (1‑based).
                int[] allPages = Enumerable.Range(1, doc.Pages.Count).ToArray();

                // Use PdfPageEditor to set the zoom factor.
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    editor.BindPdf(inputPath);
                    editor.ProcessPages = allPages;   // apply to every page
                    editor.Zoom = 1.0f;               // default scaling (100%)
                    editor.Save(outputPath);
                }
            }

            Console.WriteLine($"Zoom set to 1.0 on all pages. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
