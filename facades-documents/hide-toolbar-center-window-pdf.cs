using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "ToolbarCentered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF with PdfContentEditor.
        var editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Hide the toolbar and center the window. In the current Aspose.PDF API the
        // ChangeViewerPreference method accepts a single ViewerPreference argument –
        // calling it toggles the preference to true.
        editor.ChangeViewerPreference(ViewerPreference.HideToolbar);
        editor.ChangeViewerPreference(ViewerPreference.CenterWindow);

        // Save the modified PDF.
        editor.Save(outputPath);

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
