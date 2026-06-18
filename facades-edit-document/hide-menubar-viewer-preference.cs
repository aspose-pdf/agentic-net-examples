using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_hide_menubar.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfContentEditor works directly with file paths; no need for a Document instance.
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the source PDF.
        editor.BindPdf(inputPath);

        // Apply the HideMenubar viewer preference.
        editor.ChangeViewerPreference(ViewerPreference.HideMenubar);

        // Save the modified PDF.
        editor.Save(outputPath);

        Console.WriteLine($"Viewer preference applied. Output saved to '{outputPath}'.");
    }
}