using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_continuous.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the content editor facade, bind the PDF, set continuous view, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);
            // Continuous scrolling = one column layout.
            editor.ChangeViewerPreference(ViewerPreference.PageLayoutOneColumn);
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with continuous view mode: '{outputPath}'.");
    }
}