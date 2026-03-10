using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "sample.pdf";
        const string outputPath = "sample_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the PdfContentEditor facade
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Retrieve the current viewer preference flags
        int prefValue = editor.GetViewerPreference();

        // Example: check if the document is set to hide the menu bar
        if ((prefValue & ViewerPreference.HideMenubar) != 0)
        {
            Console.WriteLine("Menu bar is currently hidden.");
        }
        else
        {
            Console.WriteLine("Menu bar is visible.");
        }

        // Example: enable the "FitWindow" flag (resize window to fit first page)
        int newPref = prefValue | ViewerPreference.FitWindow;
        editor.ChangeViewerPreference(newPref);

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Viewer preferences updated and saved to '{outputPath}'.");
    }
}