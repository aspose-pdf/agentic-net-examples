using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfContentEditor and ViewerPreference are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the facade, bind the PDF, modify the viewer preference, and save
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Hide UI elements such as scrollbars, toolbars, and menu bars for a cleaner display
        editor.ChangeViewerPreference(ViewerPreference.HideWindowUI);

        // Persist the changes to a new file
        editor.Save(outputPath);
        editor.Close(); // Release any resources held by the facade

        Console.WriteLine($"PDF saved with hidden UI to '{outputPath}'.");
    }
}