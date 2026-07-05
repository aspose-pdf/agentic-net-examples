using System;
using System.IO;
using Aspose.Pdf;                 // For ExtractImageMode enum
using Aspose.Pdf.Facades;        // For PdfExtractor

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string imagesFolder = "images";
        const string markdownFile = "gallery.md";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the folder for extracted images exists
        Directory.CreateDirectory(imagesFolder);

        // Use PdfExtractor inside a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // Extract only images that are actually shown on the pages (optional)
            extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;

            // Start the extraction process
            extractor.ExtractImage();

            int imageIndex = 1;

            // Open the markdown file for writing
            using (StreamWriter mdWriter = new StreamWriter(markdownFile, false))
            {
                // Loop through all extracted images
                while (extractor.HasNextImage())
                {
                    string imageFileName = $"image-{imageIndex}.png";
                    string imagePath = Path.Combine(imagesFolder, imageFileName);

                    // Save the next image to a file (default format based on extension)
                    extractor.GetNextImage(imagePath);

                    // Write a markdown image link (relative path)
                    mdWriter.WriteLine($"![]({Path.Combine(imagesFolder, imageFileName)})");

                    imageIndex++;
                }
            }
        }

        Console.WriteLine($"Markdown gallery created at '{markdownFile}'.");
    }
}