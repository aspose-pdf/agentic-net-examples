using System;
using System.IO;
using Aspose.Pdf;

class ReplaceVectorWithRaster
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document (using the recommended load pattern)
        using (Document doc = new Document(inputPdf))
        {
            // FlattenTransparency replaces transparent content and vector graphics
            // with rasterized equivalents while keeping the visual layout unchanged.
            // This effectively converts vector drawings on each page to raster images.
            doc.FlattenTransparency();

            // Save the modified document (using the recommended save pattern)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Vector graphics have been rasterized and saved to '{outputPdf}'.");
    }
}