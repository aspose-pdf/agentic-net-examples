using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class PreserveVectorGraphicsCoordinateSystem
{
    static void Main()
    {
        // Input PDF containing vector graphics.
        const string inputPdf = "input.pdf";

        // Output directory for SVG files (one per page).
        const string outputDir = "SvgOutput";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Load the PDF document.
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure SVG save options.
            // ScaleToPixels = false preserves the original PDF coordinate system
            // (points) in the generated SVG, avoiding conversion to pixel units.
            SvgSaveOptions svgOptions = new SvgSaveOptions
            {
                ScaleToPixels = false
            };

            // Save each page as an individual SVG file.
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string svgPath = System.IO.Path.Combine(outputDir, $"Page_{pageNum}.svg");

                // The Save method with SvgSaveOptions respects the ScaleToPixels flag.
                pdfDoc.Save(svgPath, svgOptions);
                Console.WriteLine($"Page {pageNum} saved as SVG: {svgPath}");
            }
        }
    }
}
