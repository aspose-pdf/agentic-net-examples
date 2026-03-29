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
        editor.ChangeViewerPreference(ViewerPreference.PageLayoutSinglePage);
        editor.Save(outputPath);
        Console.WriteLine($"Viewer preference set to single‑page layout and saved to '{outputPath}'.");
    }
}
