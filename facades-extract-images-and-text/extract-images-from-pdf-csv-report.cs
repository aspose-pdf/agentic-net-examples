using System;
using System.IO;
using System.Drawing.Imaging; // for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Directory to store extracted images
        const string imagesDir = "ExtractedImages";

        // Output CSV file listing image details
        const string csvPath = "image_report.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the images directory exists
        Directory.CreateDirectory(imagesDir);

        // Prepare CSV writer
        using (var csvWriter = new StreamWriter(csvPath))
        {
            // Write CSV header
            csvWriter.WriteLine("Filename,PageNumber,Width,Height");

            // Open the PDF document (lifecycle rule: use Document constructor with file path)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    // Absorb image placements on the current page
                    var imageAbsorber = new Aspose.Pdf.ImagePlacementAbsorber();

                    // Accept the absorber for the page
                    pdfDoc.Pages[pageNum].Accept(imageAbsorber);

                    int imageIndex = 1;

                    // Process each image found on the page
                    foreach (ImagePlacement imgPlacement in imageAbsorber.ImagePlacements)
                    {
                        // Build a unique filename for the extracted image
                        string imageFileName = $"page{pageNum}_img{imageIndex}.png";
                        string imagePath = Path.Combine(imagesDir, imageFileName);

                        // Save the image resource to a file (PNG format)
                        using (FileStream imgStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                        {
                            imgPlacement.Image.Save(imgStream, ImageFormat.Png);
                        }

                        // Retrieve the displayed dimensions of the image on the page
                        double width = imgPlacement.Rectangle.Width;
                        double height = imgPlacement.Rectangle.Height;

                        // Write a line to the CSV: filename, page number, width, height
                        csvWriter.WriteLine($"{imageFileName},{pageNum},{width},{height}");

                        imageIndex++;
                    }
                }
            }

            Console.WriteLine($"Image extraction complete. Details saved to '{csvPath}'.");
        }
    }
}