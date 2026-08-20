using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfContentEditor, ViewerPreference

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "centered_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfContentEditor implements IDisposable, so wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Set the CenterWindow viewer preference (true) by applying the corresponding flag.
            editor.ChangeViewerPreference(ViewerPreference.CenterWindow);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with CenterWindow enabled: {outputPath}");
    }
}