using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string csvPath = "images.csv";
        const string imagesDir = "ExtractedImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(imagesDir);

        // Prepare CSV header
        StringBuilder csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("Filename,PageNumber,Width,Height");

        // Get total page count
        int totalPages;
        try
        {
            using (var doc = new Document(pdfPath))
            {
                totalPages = doc.Pages.Count;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to open PDF: {ex.Message}");
            return;
        }

        // Loop through each page and extract images
        for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);
                // Restrict extraction to the current page
                extractor.StartPage = pageNumber;
                extractor.EndPage = pageNumber;
                // Extract images defined in resources
                extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Build a unique filename for the image
                    string fileName = $"image_page{pageNumber}_{imageIndex}.png";
                    string outPath = Path.Combine(imagesDir, fileName);

                    // Save the image to disk
                    extractor.GetNextImage(outPath, ImageFormat.Png);

                    // Retrieve image dimensions by loading the saved file
                    int width, height;
                    using (System.Drawing.Image img = System.Drawing.Image.FromFile(outPath))
                    {
                        width = img.Width;
                        height = img.Height;
                    }

                    // Append information to CSV
                    csvBuilder.AppendLine($"{fileName},{pageNumber},{width},{height}");

                    imageIndex++;
                }
            }
        }

        // Write CSV file
        File.WriteAllText(csvPath, csvBuilder.ToString(), Encoding.UTF8);
        Console.WriteLine($"Images extracted to '{imagesDir}'. CSV report saved to '{csvPath}'.");
    }
}
