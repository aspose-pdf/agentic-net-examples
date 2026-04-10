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

        // Bind the PDF, change the viewer preference to hide the menu bar, and save.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"PDF saved with hidden menubar: '{outputPath}'.");
    }
}