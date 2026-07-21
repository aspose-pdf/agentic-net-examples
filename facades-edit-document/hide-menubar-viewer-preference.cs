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

        // PdfContentEditor is a Facades class used to modify PDF viewer preferences.
        PdfContentEditor editor = new PdfContentEditor();

        // Load the source PDF.
        editor.BindPdf(inputPath);

        // Set the viewer preference to hide the menu bar when the document is opened.
        editor.ChangeViewerPreference(ViewerPreference.HideMenubar);

        // Save the modified PDF.
        editor.Save(outputPath);

        Console.WriteLine($"PDF saved with HideMenubar preference: {outputPath}");
    }
}