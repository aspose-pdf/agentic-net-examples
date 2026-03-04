using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "example.pdf";
        const string outputPath = "example_out.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the content editor facade
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF file into the editor
        editor.BindPdf(inputPath);

        // Apply desired viewer preferences (flags can be combined with bitwise OR)
        editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
        editor.ChangeViewerPreference(ViewerPreference.PageModeUseNone);

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"Viewer preferences applied and saved to '{outputPath}'.");
    }
}