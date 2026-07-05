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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the content editor, bind the PDF, modify viewer preferences, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Example: hide the menu bar and disable any page mode outline.
            editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
            editor.ChangeViewerPreference(ViewerPreference.PageModeUseNone);

            // Save the edited PDF to a new file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}