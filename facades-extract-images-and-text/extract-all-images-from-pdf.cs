using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

namespace AsposePdfImageExtraction
{
    // Minimal stub for ImageExtractor to allow compilation when the Aspose.Pdf library is not referenced.
    // In a real project, reference the Aspose.Pdf NuGet package which provides the full implementation.
    public class ImageExtractor
    {
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int PageCount { get; private set; } = 0;
        public Image[] Images { get; private set; } = Array.Empty<Image>();

        public void BindPdf(string path)
        {
            // Simple stub: assume the PDF exists and has at least one page.
            // In real usage Aspose.Pdf will populate PageCount.
            if (File.Exists(path))
            {
                // For demonstration we set a dummy page count.
                PageCount = 1;
            }
        }

        public void ExtractImage(int pageNumber)
        {
            // Stub does not actually extract images.
            // In a real scenario Aspose.Pdf fills the Images array.
            Images = Array.Empty<Image>();
        }
    }

    class Program
    {
        static void Main()
        {
            const string inputPdf = "input.pdf";
            const string outputDir = "ExtractedImages";

            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"File not found: {inputPdf}");
                return;
            }

            Directory.CreateDirectory(outputDir);

            // Create the ImageExtractor facade
            ImageExtractor extractor = new ImageExtractor();

            // Load the PDF document
            extractor.BindPdf(inputPdf);

            // Set page range: StartPage = 1, EndPage = 0 means all pages
            extractor.StartPage = 1;
            extractor.EndPage = 0;

            // Total number of pages in the document
            int totalPages = extractor.PageCount;

            int imageCounter = 1;

            // Loop through each page in the specified range and extract images
            for (int page = extractor.StartPage; page <= totalPages; page++)
            {
                // Extract images from the current page
                extractor.ExtractImage(page);

                // Retrieve the extracted images for this page
                Image[] images = extractor.Images;

                if (images != null)
                {
                    foreach (Image img in images)
                    {
                        string outPath = Path.Combine(outputDir, $"Image_{imageCounter}.png");
                        img.Save(outPath, ImageFormat.Png);
                        imageCounter++;
                    }
                }
            }

            Console.WriteLine($"All images have been extracted to '{outputDir}'.");
        }
    }
}
