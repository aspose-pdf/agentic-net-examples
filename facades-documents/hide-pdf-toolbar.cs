using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "ToolbarHidden.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the facade and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Change the viewer preference to hide the toolbar
        editor.ChangeViewerPreference(ViewerPreference.HideToolbar);

        // Save the modified PDF
        editor.Save(outputPath);

        Console.WriteLine($"PDF saved with hidden toolbar: {outputPath}");
    }
}