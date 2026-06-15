using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Vector;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Drawing; // needed for GraphicElement, ImagePlacementAbsorber, ImagePlacement

class ExtractImagesWithGraphicsAbsorber
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // 1️⃣ Use GraphicsAbsorber to collect all graphic elements on the page
                GraphicsAbsorber graphicsAbsorber = new GraphicsAbsorber();
                graphicsAbsorber.Visit(page);

                // Filter out only image elements (ignore path/shape elements)
                List<GraphicElement> imageElements = graphicsAbsorber.Elements
                    .OfType<Aspose.Pdf.Image>() // Image class lives in Aspose.Pdf namespace
                    .Cast<GraphicElement>()
                    .ToList();

                // If no image elements were found, continue to next page
                if (imageElements.Count == 0)
                    continue;

                // 2️⃣ Use ImagePlacementAbsorber to obtain the actual image resources
                ImagePlacementAbsorber placementAbsorber = new ImagePlacementAbsorber();
                placementAbsorber.Visit(page);

                int imgIndex = 1;
                foreach (ImagePlacement placement in placementAbsorber.ImagePlacements)
                {
                    // Save each embedded image as JPEG. Image.Save expects a Stream, not a file name.
                    string outFile = System.IO.Path.Combine(outputFolder,
                        $"page{pageNum}_img{imgIndex}.jpg");
                    using (FileStream fs = new FileStream(outFile, FileMode.Create, FileAccess.Write))
                    {
                        placement.Image.Save(fs);
                    }
                    imgIndex++;
                }
            }
        }
    }
}
