using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "example.pdf";
        const string outputPath = "example_out.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor implements IDisposable, so wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF file into the editor.
            editor.BindPdf(inputPath);

            // Get the current viewer preference flags.
            int currentPref = editor.GetViewerPreference();
            Console.WriteLine($"Current viewer preferences: 0x{currentPref:X}");

            // Example: hide the menu bar and set page mode to none.
            int newPref = ViewerPreference.HideMenubar | ViewerPreference.PageModeUseNone;
            editor.ChangeViewerPreference(newPref);
            Console.WriteLine($"Applied new viewer preferences: 0x{newPref:X}");

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}