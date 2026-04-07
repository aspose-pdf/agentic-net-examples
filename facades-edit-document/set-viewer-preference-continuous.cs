using System;
using System.IO;
using Aspose.Pdf;
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

        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        // Set continuous layout (pages in one column)
        editor.ChangeViewerPreference(ViewerPreference.PageLayoutOneColumn);
        // Default zoom is 100% by default; no explicit zoom setting required
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Viewer preference applied and saved to '{outputPath}'.");
    }
}
