using System;
using System.IO;
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

        // Initialize the facade, bind the source PDF, change the viewer preference,
        // and save the result.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Hide navigation panels by setting PageMode to UseNone.
        editor.ChangeViewerPreference(ViewerPreference.PageModeUseNone);

        // Persist the changes.
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Viewer preference set to UseNone and saved to '{outputPath}'.");
    }
}