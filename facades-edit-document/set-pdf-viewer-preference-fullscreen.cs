using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_fullscreen.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor is a facade that can modify viewer preferences.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Activate full‑screen mode (no menu bar, toolbars, etc.).
            editor.ChangeViewerPreference(ViewerPreference.PageModeFullScreen);

            // Save the updated PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with full‑screen viewer preference: {outputPath}");
    }
}