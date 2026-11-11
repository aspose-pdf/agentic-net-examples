using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "resized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Facade class that provides page‑content manipulation features
            PdfFileEditor editor = new PdfFileEditor();

            // Define which pages to resize (1‑based indexing)
            int[] pagesToResize = new int[] { 2, 4 };

            // Create resize parameters with left/right/top/bottom margins (points).
            // The Margins overload does not support named arguments in the version
            // of Aspose.Pdf.Facades used, so pass the values positionally.
            var resizeParams = PdfFileEditor.ContentsResizeParameters.Margins(
                50, // left margin
                50, // right margin
                0,  // top margin
                0   // bottom margin
            );

            // Apply the resizing to the selected pages
            editor.ResizeContents(doc, pagesToResize, resizeParams);

            // Save the modified document (still inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}