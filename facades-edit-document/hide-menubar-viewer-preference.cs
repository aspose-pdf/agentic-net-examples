using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_hide_menubar.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor is a Facades class used to modify viewer preferences.
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the source PDF.
        editor.BindPdf(inputPath);

        // Set the HideMenubar flag using the ViewerPreference constant.
        editor.ChangeViewerPreference(ViewerPreference.HideMenubar);

        // Save the modified PDF.
        editor.Save(outputPath);

        // Release resources held by the editor.
        editor.Close();

        Console.WriteLine($"PDF saved with HideMenubar enabled: '{outputPath}'.");
    }
}