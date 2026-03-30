using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string markdownFile = "gallery.md";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            // Optional: extract only images that are actually shown on pages
            // extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;
            extractor.ExtractImage();

            int imageIndex = 1;
            using (StreamWriter writer = new StreamWriter(markdownFile, false))
            {
                writer.WriteLine("# Image Gallery");
                writer.WriteLine();

                while (extractor.HasNextImage())
                {
                    string imageFileName = $"image-{imageIndex}.png";
                    // Save the next image to a file (original format is kept; using .png extension works for most cases)
                    extractor.GetNextImage(imageFileName);

                    writer.WriteLine($"![Image {imageIndex}]({imageFileName})");
                    writer.WriteLine();
                    imageIndex++;
                }
            }
        }

        Console.WriteLine($"Markdown gallery generated: {markdownFile}");
    }
}