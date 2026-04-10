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

        // Initialize the facade, bind the source PDF, set the viewer preference,
        // and save the result.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Set the PageMode to display the document outline (bookmarks) by default.
        editor.ChangeViewerPreference(ViewerPreference.PageModeUseOutlines);

        // Save the modified PDF.
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Viewer preference updated and saved to '{outputPath}'.");
    }
}