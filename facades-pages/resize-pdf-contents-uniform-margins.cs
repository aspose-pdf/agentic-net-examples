using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Create an instance of PdfFileEditor (facade API)
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Define uniform margins of 10% on all sides.
        // MarginsPercent creates a ContentsResizeParameters object where
        // left, right, top and bottom margins are specified as percentages
        // of the original page size. The contents size will be calculated
        // automatically (100% - left% - right% for width, similarly for height).
        var resizeParams = PdfFileEditor.ContentsResizeParameters.MarginsPercent(
            left: 10,   // 10% left margin
            right: 10,  // 10% right margin
            top: 10,    // 10% top margin
            bottom: 10  // 10% bottom margin
        );

        // Resize contents of all pages (pages == null) using the defined parameters.
        // The method returns true on success.
        bool result = fileEditor.ResizeContents(
            source: inputPath,
            destination: outputPath,
            pages: null,          // null processes every page in the document
            parameters: resizeParams
        );

        // Report the outcome
        if (result)
        {
            Console.WriteLine($"Successfully resized contents with uniform margins. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to resize PDF contents.");
        }
    }
}