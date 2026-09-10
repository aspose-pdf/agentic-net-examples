using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string imagesOutputDir = "ExtractedImages";
        const string csvOutputPath = "image_index.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the images directory exists
        Directory.CreateDirectory(imagesOutputDir);

        // Open the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Absorb image placements from all pages
            ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
            doc.Pages.Accept(absorber);

            // Prepare CSV writer
            using (StreamWriter csvWriter = new StreamWriter(csvOutputPath, false))
            {
                // Write CSV header
                csvWriter.WriteLine("Filename,PageNumber,Width,Height");

                int globalImageIndex = 1;

                // Iterate over each image placement
                foreach (ImagePlacement imagePlacement in absorber.ImagePlacements)
                {
                    // Page number (1‑based)
                    int pageNumber = imagePlacement.Page.Number;

                    // Image rectangle dimensions
                    Rectangle rect = imagePlacement.Rectangle;
                    double width = rect.Width;
                    double height = rect.Height;

                    // Build a unique filename
                    string imageFileName = $"page{pageNumber}_img{globalImageIndex}.png";
                    string imageFilePath = Path.Combine(imagesOutputDir, imageFileName);

                    // Save the image to file (PNG format). XImage.Save writes the image in its native format.
                    using (FileStream imgStream = new FileStream(imageFilePath, FileMode.Create, FileAccess.Write))
                    {
                        imagePlacement.Image.Save(imgStream);
                    }

                    // Write CSV line
                    csvWriter.WriteLine($"{imageFileName},{pageNumber},{width},{height}");

                    globalImageIndex++;
                }
            }
        }

        Console.WriteLine($"Extraction complete. Images saved to '{imagesOutputDir}'. CSV saved to '{csvOutputPath}'.");
    }
}
