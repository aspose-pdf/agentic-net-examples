using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfContentEditor, ViewerPreference

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

        // Create the facade and bind the PDF file
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Retrieve the current viewer preference flags
        int currentPrefs = editor.GetViewerPreference();

        // Example: check whether the HideMenubar flag is set
        if ((currentPrefs & ViewerPreference.HideMenubar) != 0)
        {
            Console.WriteLine("HideMenubar flag is currently set.");
        }
        else
        {
            Console.WriteLine("HideMenubar flag is not set.");
        }

        // Change a viewer preference – for example, hide the toolbar
        editor.ChangeViewerPreference(ViewerPreference.HideToolbar);

        // Save the modified PDF to a new file
        editor.Save(outputPath);

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}