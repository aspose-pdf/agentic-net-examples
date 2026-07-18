using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to source and destination PDF files
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Create an instance of PdfFileEditor (does not implement IDisposable)
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Define asymmetric margins:
        //   Left  = 10% of page width
        //   Right = 20% of page width
        //   Top   = 5% of page height
        //   Bottom= 5% of page height
        //   Content width/height are set to null so they are calculated automatically
        PdfFileEditor.ContentsResizeParameters resizeParams = new PdfFileEditor.ContentsResizeParameters(
            PdfFileEditor.ContentsResizeValue.Percents(10), // left margin
            null,                                          // auto content width
            PdfFileEditor.ContentsResizeValue.Percents(20),// right margin
            PdfFileEditor.ContentsResizeValue.Percents(5), // top margin
            null,                                          // auto content height
            PdfFileEditor.ContentsResizeValue.Percents(5) // bottom margin
        );

        // Apply the resize to all pages (pages array = null)
        bool result = fileEditor.ResizeContents(inputPath, outputPath, null, resizeParams);

        // Report the outcome
        if (result)
            Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to resize PDF contents.");
    }
}