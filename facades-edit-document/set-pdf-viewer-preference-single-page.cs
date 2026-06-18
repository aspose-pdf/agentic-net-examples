using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfContentEditor and ViewerPreference are defined here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_single_page.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the content editor facade
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF document
        editor.BindPdf(inputPath);

        // Change the viewer preference to single‑page layout
        editor.ChangeViewerPreference(ViewerPreference.PageLayoutSinglePage);

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"Viewer preference set to single‑page layout. Saved to '{outputPath}'.");
    }
}