using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create an instance of PdfFileEditor (no IDisposable implementation required)
        PdfFileEditor editor = new PdfFileEditor();

        // Define uniform margins of 10% on all sides using the static MarginsPercent factory method
        PdfFileEditor.ContentsResizeParameters parameters =
            PdfFileEditor.ContentsResizeParameters.MarginsPercent(
                left: 10,   // left margin (10% of page width)
                right: 10,  // right margin (10% of page width)
                top: 10,    // top margin (10% of page height)
                bottom: 10  // bottom margin (10% of page height)
            );

        // Resize contents of all pages (null pages array means all pages)
        bool success = editor.ResizeContents(inputPath, outputPath, null, parameters);

        // Report the result
        if (success)
        {
            Console.WriteLine($"Contents resized and saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Resize operation failed.");
        }
    }
}