using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string imagesFolder = "images";
        const string markdownFile = "gallery.md";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the output folder for images exists
        Directory.CreateDirectory(imagesFolder);

        // Prepare a list to hold markdown lines
        List<string> markdownLines = new List<string>();
        markdownLines.Add("# Image Gallery");
        markdownLines.Add(string.Empty);

        // Use PdfExtractor to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            // Optional: choose a different extraction mode
            // extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;
            extractor.ExtractImage();

            int index = 1;
            while (extractor.HasNextImage())
            {
                string imageFileName = $"image-{index}.png";
                string imagePath = Path.Combine(imagesFolder, imageFileName);

                // Save the next image as PNG
                extractor.GetNextImage(imagePath, ImageFormat.Png);

                // Add a markdown entry for the saved image
                markdownLines.Add($"![Image {index}]({Path.Combine(imagesFolder, imageFileName)})");
                markdownLines.Add(string.Empty);
                index++;
            }
        }

        // Write the markdown file
        File.WriteAllLines(markdownFile, markdownLines);
        Console.WriteLine($"Extraction complete. Images saved to '{imagesFolder}'. Markdown gallery created at '{markdownFile}'.");
    }
}