using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfContentEditor and ViewerPreference are defined here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the facade that will edit the PDF's viewer preferences
        PdfContentEditor editor = new PdfContentEditor();

        try
        {
            // Load the PDF file into the editor
            editor.BindPdf(inputPath);

            // Change viewer preferences as required.
            // Example: hide the menu bar and disable any page mode outline.
            editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
            editor.ChangeViewerPreference(ViewerPreference.PageModeUseNone);

            // Save the edited PDF to a new file path
            editor.Save(outputPath);
        }
        finally
        {
            // Ensure resources are released
            editor.Close();
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}