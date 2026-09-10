using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_hide_toolbar.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (a Facade) to modify viewer preferences.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF file.
            editor.BindPdf(inputPath);

            // Set the HideToolbar flag (value defined in ViewerPreference).
            editor.ChangeViewerPreference(ViewerPreference.HideToolbar);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with HideToolbar set: {outputPath}");
    }
}