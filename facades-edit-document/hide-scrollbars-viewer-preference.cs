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

        // Initialize the facade, bind the source PDF, and set the viewer preference.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Hide UI elements such as scrollbars, toolbars, and menu bars.
        // ViewerPreference.HideWindowUI hides all UI elements, providing a cleaner view.
        editor.ChangeViewerPreference(ViewerPreference.HideWindowUI);

        // Save the modified PDF.
        editor.Save(outputPath);

        Console.WriteLine($"Viewer preference applied and saved to '{outputPath}'.");
    }
}