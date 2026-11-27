using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load PDF document (wrapped in using for deterministic disposal)
            using (Document doc = new Document(inputPath))
            {
                int savedImageCount = 0;

                // Pages are 1‑based in Aspose.Pdf
                for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
                {
                    Page page = doc.Pages[pageNumber];

                    // Use ImagePlacementAbsorber to collect images on the page
                    ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                    page.Accept(absorber);

                    foreach (ImagePlacement imgPlacement in absorber.ImagePlacements)
                    {
                        XImage xImg = imgPlacement.Image;
                        if (xImg != null)
                        {
                            savedImageCount++;
                            string outPath = Path.Combine(outputDir,
                                $"page{pageNumber}_img{savedImageCount}.png");

                            // XImage.Save expects a Stream, not a file path string
                            using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                            {
                                xImg.Save(fs);
                            }
                            Console.WriteLine($"Saved image: {outPath}");
                        }
                    }
                }

                Console.WriteLine($"Extraction complete. Total images saved: {savedImageCount}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
