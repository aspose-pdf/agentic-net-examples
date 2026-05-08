using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPortfolio = "portfolio.pdf";
        const string tempFolder = "temp_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure a clean temporary folder
        if (Directory.Exists(tempFolder))
            Directory.Delete(tempFolder, true);
        Directory.CreateDirectory(tempFolder);

        // ---------- Extract images from the source PDF ----------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each extracted image as a separate file (default format is JPEG)
                string imagePath = Path.Combine(tempFolder, $"image_{imageIndex}.jpg");
                extractor.GetNextImage(imagePath);
                imageIndex++;
            }
        }

        // ---------- Build a PDF portfolio where each image occupies one page ----------
        using (Document portfolio = new Document())
        {
            foreach (string imageFile in Directory.GetFiles(tempFolder))
            {
                // Add a new blank page
                Page page = portfolio.Pages.Add();

                // Create an Image object that references the extracted file
                Image img = new Image();
                img.File = imageFile;

                // Add the image to the page's content
                page.Paragraphs.Add(img);
            }

            // Save the resulting portfolio PDF
            portfolio.Save(outputPortfolio);
        }

        // Cleanup temporary image files
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – if cleanup fails, the OS will eventually reclaim the temp folder
        }

        Console.WriteLine($"Portfolio PDF created at '{outputPortfolio}'.");
    }
}