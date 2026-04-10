using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_asymmetric.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define asymmetric margins as percentages of the original page size
            var leftMargin   = PdfFileEditor.ContentsResizeValue.Percents(5);   // 5% left
            var rightMargin  = PdfFileEditor.ContentsResizeValue.Percents(15);  // 15% right
            var topMargin    = PdfFileEditor.ContentsResizeValue.Percents(10);  // 10% top
            var bottomMargin = PdfFileEditor.ContentsResizeValue.Percents(10);  // 10% bottom

            // Create resize parameters: specify margins, let content width/height be calculated automatically (null)
            PdfFileEditor.ContentsResizeParameters resizeParams = new PdfFileEditor.ContentsResizeParameters(
                leftMargin,    // left margin
                null,          // content width (auto)
                rightMargin,   // right margin
                topMargin,     // top margin
                null,          // content height (auto)
                bottomMargin   // bottom margin
            );

            // Perform the resize on all pages (pages argument = null)
            PdfFileEditor editor = new PdfFileEditor();
            editor.ResizeContents(doc, null, resizeParams);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}