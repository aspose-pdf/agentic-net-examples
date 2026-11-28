using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        Directory.CreateDirectory(outputDir);

        // Get the total number of pages using Document (PdfExtractor has no PageCount property)
        int pageCount;
        using (var doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        int imageIndex = 0;

        // PdfExtractor implements IDisposable; ensure deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(inputPath);

            // Enable extraction of images that are defined in the PDF resources
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Iterate through each page and extract images
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                // Restrict extraction to the current page
                extractor.StartPage = pageNumber;
                extractor.EndPage   = pageNumber;
                extractor.ExtractImage();

                while (extractor.HasNextImage())
                {
                    // Retrieve the image data into a memory stream
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imgStream);
                        imgStream.Position = 0;

                        // Build a filename that includes the page number and a sequential index
                        string fileName = $"page_{pageNumber}_img_{++imageIndex}.png";
                        string outPath = Path.Combine(outputDir, fileName);

                        // Save the image bytes as a PNG file
                        File.WriteAllBytes(outPath, imgStream.ToArray());

                        Console.WriteLine($"Saved image: {outPath}");
                    }
                }
            }
        }
    }
}
