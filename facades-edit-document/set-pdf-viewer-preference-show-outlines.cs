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

        // Load the PDF, set the viewer preference to show outlines, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);
            // Configure PageMode to display the document outline panel by default.
            editor.ChangeViewerPreference(ViewerPreference.PageModeUseOutlines);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Viewer preference set to UseOutlines and saved to '{outputPath}'.");
    }
}