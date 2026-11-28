using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the document to know the total page count
        Document doc = new Document(inputPdf);
        int totalPages = doc.Pages.Count;

        // PdfExtractor implements IDisposable; ensure deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF document once (the same instance can be reused for each page range)
            extractor.BindPdf(inputPdf);

            // Extract images defined in the PDF resources (skip inline images)
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Process each page individually so we can prefix the file name with the page number
            for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
            {
                // Set the page range to a single page
                extractor.StartPage = pageNumber;
                extractor.EndPage   = pageNumber;

                // Perform the extraction for the current page
                extractor.ExtractImage();

                int imageIndex = 1;
                // Iterate through all images found on this page
                while (extractor.HasNextImage())
                {
                    string fileName = $"Page_{pageNumber}_Image_{imageIndex}.png";
                    string outPath = Path.Combine(outputDir, fileName);

                    // Save the image directly to a file (no System.Drawing.Image needed)
                    extractor.GetNextImage(outPath);

                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
