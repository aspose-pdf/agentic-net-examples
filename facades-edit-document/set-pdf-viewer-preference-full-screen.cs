using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "fullScreen_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Modify viewer preferences using PdfContentEditor
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Enable full‑screen mode when the PDF is opened
            editor.ChangeViewerPreference(ViewerPreference.PageModeFullScreen);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with full‑screen viewer preference: {outputPath}");
    }
}