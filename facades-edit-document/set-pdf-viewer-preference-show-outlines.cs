using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_outlines.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor is a Facades class used to modify viewer preferences.
        PdfContentEditor editor = new PdfContentEditor();
        try
        {
            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Set the PageMode viewer preference to display the document outline panel by default.
            editor.ChangeViewerPreference(ViewerPreference.PageModeUseOutlines);

            // Save the modified PDF.
            editor.Save(outputPath);
        }
        finally
        {
            // Ensure resources are released.
            editor.Close();
        }

        Console.WriteLine($"Viewer preference updated and saved to '{outputPath}'.");
    }
}