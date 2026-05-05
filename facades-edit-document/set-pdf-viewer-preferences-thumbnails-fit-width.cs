using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_viewer_pref.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the content editor facade, bind the PDF, set viewer preferences, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF file.
            editor.BindPdf(inputPath);

            // Show the thumbnail pane when the document is opened.
            editor.ChangeViewerPreference(ViewerPreference.PageModeUseThumbs);

            // Resize the window to fit the first page (helps achieve a fit‑width view).
            editor.ChangeViewerPreference(ViewerPreference.FitWindow);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with viewer preferences: '{outputPath}'");
    }
}