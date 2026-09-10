using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_asymmetric.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a PdfFileEditor instance (does not implement IDisposable)
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Define asymmetric margins:
        // Left margin = 10% of page width
        // Right margin = 20% of page width
        // Top margin = 5% of page height
        // Bottom margin = 5% of page height
        // Content width and height are set to auto (null) so they are calculated automatically.
        Aspose.Pdf.Facades.PdfFileEditor.ContentsResizeParameters parameters =
            new Aspose.Pdf.Facades.PdfFileEditor.ContentsResizeParameters(
                Aspose.Pdf.Facades.PdfFileEditor.ContentsResizeValue.Percents(10), // left margin
                null, // auto content width
                Aspose.Pdf.Facades.PdfFileEditor.ContentsResizeValue.Percents(20), // right margin
                Aspose.Pdf.Facades.PdfFileEditor.ContentsResizeValue.Percents(5),  // top margin
                null, // auto content height
                Aspose.Pdf.Facades.PdfFileEditor.ContentsResizeValue.Percents(5)   // bottom margin
            );

        // Resize all pages (null pages array) using the asymmetric parameters
        bool success = fileEditor.ResizeContents(inputPath, outputPath, null, parameters);

        if (success)
        {
            Console.WriteLine($"Resizing completed. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Resizing failed.");
        }
    }
}