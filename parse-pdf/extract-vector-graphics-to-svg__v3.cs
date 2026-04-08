using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // GraphicsAbsorber resides in this namespace in recent versions
using Aspose.Pdf.Drawing; // for Graphic and related drawing types

class PreserveVectorGraphicsToSvg
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF containing vector graphics
        const string outputDir = "ExtractedSvg";       // folder where SVG files will be written

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (no special load options required for vector extraction)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Absorb all graphic elements on the current page
                GraphicsAbsorber absorber = new GraphicsAbsorber();
                page.Accept(absorber);

                // If the page contains no vector graphics, skip it
                if (absorber.Graphics.Count == 0)
                    continue;

                // Export each graphic element to an individual SVG file.
                // SaveToSvg() returns the SVG string while preserving the original
                // coordinate system (no scaling or transformation is applied).
                int elementIdx = 1;
                foreach (Graphic graphic in absorber.Graphics)
                {
                    string svgContent = graphic.SaveToSvg();

                    // Build a file name that reflects page and element order
                    string svgFileName = Path.Combine(
                        outputDir,
                        $"page_{pageIndex}_element_{elementIdx}.svg");

                    File.WriteAllText(svgFileName, svgContent);
                    Console.WriteLine($"Saved SVG: {svgFileName}");

                    elementIdx++;
                }
            }
        }

        Console.WriteLine("Vector graphics extraction completed.");
    }
}
