using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF containing images
        const string outputPdf = "portfolio.pdf";      // resulting PDF with one image per page

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // Extract images from the source PDF using PdfExtractor (Facade API)
        // -----------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);          // bind the source PDF
            extractor.ExtractImage();             // prepare image extraction

            // -------------------------------------------------------------
            // Create a new PDF document that will hold the extracted images
            // -------------------------------------------------------------
            using (Document portfolio = new Document())
            {
                // Loop through all extracted images
                while (extractor.HasNextImage())
                {
                    // Retrieve the next image into a memory stream
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imgStream);
                        imgStream.Position = 0;   // reset for reading

                        // Add a new blank page to the portfolio document
                        Page page = portfolio.Pages.Add();

                        // Define a rectangle that covers the whole page.
                        // Using standard A4 dimensions (595 x 842 points).
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 595, 842);

                        // Place the image on the page, scaling it to fit the rectangle.
                        page.AddImage(imgStream, rect);
                    }
                }

                // Save the resulting PDF portfolio
                portfolio.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF portfolio created: {outputPdf}");
    }
}