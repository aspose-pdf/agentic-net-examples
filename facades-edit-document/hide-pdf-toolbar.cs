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

        // Load the PDF, modify viewer preferences, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF file.
            editor.BindPdf(inputPath);

            // Hide the toolbar when the document is opened.
            editor.ChangeViewerPreference(ViewerPreference.HideToolbar);

            // Save the updated PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF with hidden toolbar saved to '{outputPath}'.");
    }
}