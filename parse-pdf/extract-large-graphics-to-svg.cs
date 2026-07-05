using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPath = "input.pdf";
        // Directory where filtered SVG files will be saved
        const string outputDir = "FilteredGraphics";
        // Minimum area (in PDF points) a graphic must have to be exported
        const double minArea = 5000.0;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: use using)
        using (Document pdfDoc = new Document(inputPath))
        {
            int graphicIndex = 1; // counter for naming exported files

            // Iterate through all pages (page indexing is 1‑based)
            foreach (Page page in pdfDoc.Pages)
            {
                // Absorb all graphic elements on the current page
                using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                {
                    absorber.Visit(page);

                    // Process each absorbed graphic element
                    foreach (GraphicElement element in absorber.Elements)
                    {
                        // The bounding rectangle of the element (Aspose.Pdf.Rectangle)
                        Aspose.Pdf.Rectangle rect = element.Rectangle;
                        if (rect == null) continue; // safety check

                        // Compute width and height in points
                        double width = rect.URX - rect.LLX;
                        double height = rect.URY - rect.LLY;

                        // Export only if the area exceeds the defined threshold
                        if ((width * height) > minArea)
                        {
                            string svgPath = Path.Combine(
                                outputDir,
                                $"page_{page.Number}_graphic_{graphicIndex}.svg");

                            // Save the individual graphic element as an SVG file
                            element.SaveToSvg(svgPath);
                            graphicIndex++;
                        }
                    }
                }
            }
        }

        Console.WriteLine("Filtered graphics extraction completed.");
    }
}