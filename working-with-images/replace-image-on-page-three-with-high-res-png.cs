using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // ImagePlacementAbsorber resides in Facades namespace

class ReplaceImageOnPageThree
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF
        const string highResPngPath = "high_res.png"; // replacement PNG
        const string outputPdfPath = "output.pdf";    // result PDF

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(highResPngPath))
        {
            Console.Error.WriteLine($"Replacement PNG not found: {highResPngPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least three pages (page indexing is 1‑based)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("Document does not contain a third page.");
                return;
            }

            // Get page three
            Page pageThree = doc.Pages[3];

            // Absorb all image placements on page three
            ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
            pageThree.Accept(absorber);

            // Replace each found image with the higher‑resolution PNG
            foreach (ImagePlacement imgPlacement in absorber.ImagePlacements)
            {
                // Open the PNG stream for each replacement
                using (FileStream pngStream = File.OpenRead(highResPngPath))
                {
                    imgPlacement.Replace(pngStream);
                }
            }

            // Save the modified document (lifecycle rule: save inside using block)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image on page 3 replaced and saved to '{outputPdfPath}'.");
    }
}