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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfFileEditor instance (does not implement IDisposable)
            PdfFileEditor editor = new PdfFileEditor();

            // Define resize parameters: 10% margins on all sides.
            // MarginsPercent creates parameters with automatic content size calculation.
            PdfFileEditor.ContentsResizeParameters parameters =
                PdfFileEditor.ContentsResizeParameters.MarginsPercent(
                    left:   10,   // left margin 10%
                    right:  10,   // right margin 10%
                    top:    10,   // top margin 10%
                    bottom: 10);  // bottom margin 10%

            // Apply the resize to all pages (null pages array means all pages)
            editor.ResizeContents(doc, parameters);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}