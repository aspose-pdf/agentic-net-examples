using System;
using System.IO;
using Aspose.Pdf;
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

        // Edit PDF viewer preferences using PdfContentEditor (facade API)
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Hide the toolbar when the document is opened
            editor.ChangeViewerPreference(ViewerPreference.HideToolbar);

            // Persist the changes
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden toolbar: '{outputPath}'.");
    }
}
