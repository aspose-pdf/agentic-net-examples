using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "portfolio.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Extract images from the source PDF and build a new PDF where each image occupies its own page.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            // Optional: extract only images actually rendered on pages.
            // extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;
            extractor.ExtractImage();

            using (Document portfolio = new Document())
            {
                while (extractor.HasNextImage())
                {
                    // Retrieve the next image into a memory stream.
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imgStream);
                        imgStream.Position = 0; // reset for reading

                        // Add a new page for this image.
                        portfolio.Pages.Add();
                        Page page = portfolio.Pages[portfolio.Pages.Count]; // 1‑based indexing

                        // Create an Image object from the stream and place it on the page.
                        Image img = new Image
                        {
                            ImageStream = imgStream
                        };
                        page.Paragraphs.Add(img);
                    }
                }

                // Save the resulting PDF portfolio.
                portfolio.Save(outputPdf);
            }
        }

        Console.WriteLine($"Portfolio PDF created at '{outputPdf}'.");
    }
}