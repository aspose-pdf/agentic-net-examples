using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector; // GraphicsAbsorber lives in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string csvReportPath = "graphics_report.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least 6 pages (page-indexing-one-based rule)
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("Document does not contain page 6.");
                return;
            }

            // Get page 6 (1‑based indexing)
            Page pageSix = doc.Pages[6];

            // Use GraphicsAbsorber from Aspose.Pdf.Vector to collect graphic elements on the page
            using (GraphicsAbsorber graphicsAbsorber = new GraphicsAbsorber())
            {
                graphicsAbsorber.Visit(pageSix);

                // Prepare CSV writer
                using (StreamWriter writer = new StreamWriter(csvReportPath, false))
                {
                    // CSV header
                    writer.WriteLine("ElementIndex,LLX,LLY,URX,URY");

                    // Iterate through each graphic element
                    for (int i = 0; i < graphicsAbsorber.Elements.Count; i++)
                    {
                        // Each element is a GraphicElement; its bounding rectangle is Aspose.Pdf.Rectangle
                        Rectangle rect = graphicsAbsorber.Elements[i].Rectangle;

                        // Write coordinates to CSV (comma‑separated)
                        writer.WriteLine($"{i + 1},{rect.LLX},{rect.LLY},{rect.URX},{rect.URY}");
                    }
                }
            }

            Console.WriteLine($"Graphics report saved to '{csvReportPath}'.");
        }
    }
}
