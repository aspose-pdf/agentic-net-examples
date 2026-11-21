using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "resized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create the facade that will perform the content resize operation
            PdfFileEditor editor = new PdfFileEditor();

            // Configure margins using point values only (Length class not available in this version)
            // Left: 10 points, Right: 5 points, Top: 15 points, Bottom: 3 points
            PdfFileEditor.ContentsResizeParameters resizeParams =
                PdfFileEditor.ContentsResizeParameters.Margins(10, 5, 15, 3);

            // Apply the resize operation to all pages of the document
            editor.ResizeContents(doc, resizeParams);

            // Save the modified PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
