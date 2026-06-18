using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the PdfContentEditor facade, bind the PDF, apply changes,
        // retrieve viewer preferences after each change, and finally save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document into the editor.
            editor.BindPdf(inputPath);

            // First edit operation: hide the menu bar.
            editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
            int prefAfterHideMenubar = editor.GetViewerPreference();
            Console.WriteLine($"Viewer preferences after HideMenubar: 0x{prefAfterHideMenubar:X}");

            // Second edit operation: set page mode to none.
            editor.ChangeViewerPreference(ViewerPreference.PageModeUseNone);
            int prefAfterPageMode = editor.GetViewerPreference();
            Console.WriteLine($"Viewer preferences after PageModeUseNone: 0x{prefAfterPageMode:X}");

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}