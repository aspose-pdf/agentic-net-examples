using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "edited_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Edit viewer preferences using PdfContentEditor (facade)
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF file
            editor.BindPdf(inputPath);

            // Change desired viewer preferences
            editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
            editor.ChangeViewerPreference(ViewerPreference.PageModeUseNone);

            // Save the modified PDF to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated viewer preferences to '{outputPath}'.");
    }
}