using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a PdfContentEditor facade, bind the PDF, change viewer preferences,
        // and save the result to a new file.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Example viewer preferences: hide the menu bar and disable any page mode outline.
        editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
        editor.ChangeViewerPreference(ViewerPreference.PageModeUseNone);

        // Save the edited document.
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}