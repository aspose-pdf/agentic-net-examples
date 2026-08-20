using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputSvgPath = "extracted_page1.svg";
        const int pageNumber = 1;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            if (!page.HasVectorGraphics())
            {
                Console.WriteLine("The specified page contains no vector graphics.");
                return;
            }

            // Collect vector graphics from the page
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(page);

            // Convert collected graphics to SVG
            SvgExtractor extractor = new SvgExtractor();
            string svgContent = extractor.Extract(absorber, g => true, page);

            // Save SVG for further manipulation
            File.WriteAllText(outputSvgPath, svgContent);
            Console.WriteLine($"Vector graphics extracted to: {outputSvgPath}");
        }
    }
}