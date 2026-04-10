using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_single_page.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor to modify viewer preferences
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Set the viewer preference to single‑page layout
            editor.ChangeViewerPreference(ViewerPreference.PageLayoutSinglePage);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Viewer preference set to single‑page layout. Saved to '{outputPath}'.");
    }
}