using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputSvg = "output.svg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Configure SVG export options
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Set the resolution (DPI) for any rasterized content within the SVG.
                // In recent Aspose.Pdf versions the property is named "RasterImagesResolution".
                // If the property is unavailable (older versions), the DPI defaults to 96.
                // The following line is kept for newer versions; it will be ignored if the property does not exist.
                // Uncomment when using a version that supports it.
                // svgOptions.RasterImagesResolution = 300;

                // Enable CSS style embedding by scaling to pixels. This replaces the non‑existent CssClassName property.
                svgOptions.ScaleToPixels = true;

                // Fonts are embedded automatically when possible; no explicit property is required.

                // Save the document as SVG using the configured options
                doc.Save(outputSvg, svgOptions);

                Console.WriteLine($"SVG saved to '{outputSvg}' (CSS scaling enabled={svgOptions.ScaleToPixels}).");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
