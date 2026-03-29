using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);
            // Set continuous view mode (pages displayed in one column)
            editor.ChangeViewerPreference(ViewerPreference.PageLayoutOneColumn);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Viewer preference set and saved to '{outputPath}'.");
    }
}