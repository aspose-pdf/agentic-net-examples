using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string imagesFolder = "images";         // folder to store extracted images
        const string markdownFile = "gallery.md";     // output markdown file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the images folder exists
        Directory.CreateDirectory(imagesFolder);

        // Use PdfExtractor (Facade) to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file
            extractor.BindPdf(inputPdf);

            // Optional: set resolution for clearer images (default is 150)
            extractor.Resolution = 150;

            // Start the image extraction process
            extractor.ExtractImage();

            int imageIndex = 1;
            List<string> markdownLines = new List<string>
            {
                "# Image Gallery",
                ""
            };

            // Retrieve each image until none are left
            while (extractor.HasNextImage())
            {
                // Build a file name for the extracted image
                string imagePath = Path.Combine(imagesFolder, $"image-{imageIndex}.jpg");

                // Save the next image to the file (default format is JPEG)
                extractor.GetNextImage(imagePath);

                // Add a markdown entry for the image
                markdownLines.Add($"![Image {imageIndex}]({imagePath})");

                imageIndex++;
            }

            // Write the markdown content to the output file
            File.WriteAllLines(markdownFile, markdownLines);
        }

        Console.WriteLine($"Image extraction complete. Markdown gallery saved to '{markdownFile}'.");
    }
}