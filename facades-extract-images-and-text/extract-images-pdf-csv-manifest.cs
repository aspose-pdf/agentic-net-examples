using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facade namespace is allowed for other operations if needed

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";
        const string csvManifestPath = "image_manifest.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the folder for extracted images exists
        Directory.CreateDirectory(outputFolder);

        // Prepare CSV writer
        using (var csvWriter = new StreamWriter(csvManifestPath, false))
        {
            // Write CSV header
            csvWriter.WriteLine("FileName,PageNumber,Width,Height");

            // Open the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Iterate through all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    // Search for image placements on the current page
                    var absorber = new ImagePlacementAbsorber();
                    pdfDoc.Pages[pageNum].Accept(absorber);

                    int imageIndex = 1;
                    foreach (ImagePlacement placement in absorber.ImagePlacements)
                    {
                        // Build a unique file name for each extracted image
                        string imageFileName = $"image_page{pageNum}_img{imageIndex}.bin";
                        string imagePath = Path.Combine(outputFolder, imageFileName);

                        // Save the original image bytes (no System.Drawing dependency)
                        using (FileStream imgStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                        {
                            placement.Image.Save(imgStream);
                        }

                        // Retrieve displayed dimensions from the placement rectangle
                        Aspose.Pdf.Rectangle rect = placement.Rectangle;
                        double width = rect.Width;
                        double height = rect.Height;

                        // Write a line to the CSV manifest
                        csvWriter.WriteLine($"{imageFileName},{pageNum},{width},{height}");

                        imageIndex++;
                    }
                }
            }
        }

        Console.WriteLine($"Image extraction complete. Manifest saved to '{csvManifestPath}'.");
    }
}