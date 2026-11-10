using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "portfolio.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Extract all images from the source PDF using PdfExtractor (Facades API)
        List<MemoryStream> extractedImages = new List<MemoryStream>();
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage();

            while (extractor.HasNextImage())
            {
                MemoryStream imgStream = new MemoryStream();
                extractor.GetNextImage(imgStream);
                imgStream.Position = 0; // reset for later reading
                extractedImages.Add(imgStream);
            }
        }

        // Create a new PDF where each extracted image occupies its own page
        using (Document portfolioDoc = new Document())
        {
            foreach (MemoryStream imgStream in extractedImages)
            {
                // Add a blank page
                Page page = portfolioDoc.Pages.Add();

                // Create an ImageStamp from the image stream
                ImageStamp imgStamp = new ImageStamp(imgStream)
                {
                    // Scale the image to fit the page dimensions
                    Width = page.PageInfo.Width,
                    Height = page.PageInfo.Height,
                    // Center the image on the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Apply the stamp to the page
                page.AddStamp(imgStamp);
            }

            // Save the resulting PDF portfolio
            portfolioDoc.Save(outputPdfPath);
        }

        // Dispose the image streams now that the PDF is saved
        foreach (MemoryStream ms in extractedImages)
        {
            ms.Dispose();
        }

        Console.WriteLine($"Portfolio PDF created at '{outputPdfPath}'.");
    }
}
