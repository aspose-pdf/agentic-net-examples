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

        // Load the PDF and modify viewer preferences using PdfContentEditor (Facade API)
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Set the PageMode to display the document outline panel by default
            editor.ChangeViewerPreference(ViewerPreference.PageModeUseOutlines);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Viewer preference updated and saved to '{outputPath}'.");
    }
}