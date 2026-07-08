using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the facade, bind the PDF, set the viewer preference, and save.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);                                 // Load the PDF.
        editor.ChangeViewerPreference(ViewerPreference.PageModeUseNone); // Hide navigation panels.
        editor.Save(outputPath);                                   // Write the modified PDF.
        editor.Close();                                            // Release resources.

        Console.WriteLine($"Viewer preference set to UseNone and saved to '{outputPath}'.");
    }
}