using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfContentEditor and ViewerPreference

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

        // Create the facade, bind the source PDF, set the viewer preference, and save.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);                                 // load
        editor.ChangeViewerPreference(ViewerPreference.PageModeUseNone); // hide navigation panels
        editor.Save(outputPath);                                   // save
        editor.Close();                                            // release resources

        Console.WriteLine($"Viewer preference set to PageModeUseNone and saved to '{outputPath}'.");
    }
}