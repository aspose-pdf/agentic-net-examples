using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "sample.pdf";

        // Verify that the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize the PdfExtractor facade
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(pdfPath);

            // Extract only the images that are actually used on the pages
            extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;

            // Set the page range to the whole document
            extractor.StartPage = 1;
            extractor.EndPage = extractor.Document.Pages.Count;

            // Perform the image extraction
            extractor.ExtractImage();

            int imageIndex = 1;

            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Store each image in a memory stream
                using (MemoryStream imgStream = new MemoryStream())
                {
                    extractor.GetNextImage(imgStream);
                    imgStream.Position = 0; // Reset stream position for reading

                    // Example: save the image to a file (optional)
                    string outFile = $"image_{imageIndex}.png";
                    using (FileStream file = new FileStream(outFile, FileMode.Create, FileAccess.Write))
                    {
                        imgStream.CopyTo(file);
                    }

                    Console.WriteLine($"Extracted image saved to {outFile}");
                    imageIndex++;
                }
            }
        }
    }
}