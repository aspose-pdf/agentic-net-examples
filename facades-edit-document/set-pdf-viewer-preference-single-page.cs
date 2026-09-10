using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "single_page_view.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor to modify viewer preferences
        PdfContentEditor editor = new PdfContentEditor();
        try
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Set the viewer preference to single‑page layout
            editor.ChangeViewerPreference(ViewerPreference.PageLayoutSinglePage);

            // Save the modified PDF
            editor.Save(outputPath);
        }
        finally
        {
            // Release resources
            editor.Close();
        }

        Console.WriteLine($"Viewer preference set to single‑page layout. Saved to '{outputPath}'.");
    }
}