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
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        int preference = ViewerPreference.NonFullScreenPageModeUseThumbs | ViewerPreference.FitWindow;
        editor.ChangeViewerPreference(preference);
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine("Viewer preferences updated and saved to '" + outputPath + "'.");
    }
}