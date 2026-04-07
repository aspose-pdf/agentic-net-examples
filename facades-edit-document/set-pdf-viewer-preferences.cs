using System;
using System.IO;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main()
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
        int preference = ViewerPreference.NonFullScreenPageModeUseThumbs | ViewerPreference.FitWindow;
        editor.ChangeViewerPreference(preference);
        editor.Save(outputPath);
        Console.WriteLine($"Viewer preferences applied and saved to '{outputPath}'.");
    }
}