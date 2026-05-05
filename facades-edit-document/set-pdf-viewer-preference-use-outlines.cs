using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // ViewerPreference resides here

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

        // Initialize the facade and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Change the viewer preference to show the document outline panel by default
        editor.ChangeViewerPreference(ViewerPreference.PageModeUseOutlines);

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Viewer preference set to UseOutlines and saved to '{outputPath}'.");
    }
}