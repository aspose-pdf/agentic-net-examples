using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector; // GraphicsAbsorber and SvgExtractor live here

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputSvgPath = "combined.svg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the requested page exists
            if (pdfDoc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a page 3.");
                return;
            }

            Page page = pdfDoc.Pages[3];

            // Absorb all vector graphics on the page
            using (GraphicsAbsorber absorber = new GraphicsAbsorber())
            {
                absorber.Visit(page);

                // Filter that selects every graphic element (i.e., no filtering)
                Predicate<GraphicElement> selectAll = _ => true;

                // Extract the selected graphics to a single combined SVG file
                SvgExtractor extractor = new SvgExtractor();
                extractor.Extract(absorber, selectAll, page, outputSvgPath);
            }
        }

        Console.WriteLine($"Vector elements from page 3 have been saved to '{outputSvgPath}'.");
    }
}
