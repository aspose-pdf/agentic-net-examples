using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        // Set continuous layout (one column) which shows pages in a single‑page continuous view
        editor.ChangeViewerPreference(ViewerPreference.PageLayoutOneColumn);
        // Save the updated PDF
        editor.Save(outputPath);

        Console.WriteLine("Viewer preference set and saved to " + outputPath);
    }
}