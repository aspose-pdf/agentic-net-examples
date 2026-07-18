using System;
using System.IO;
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

        // Extract images from the source PDF using PdfExtractor (Facade API)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage();

            // Create a new PDF document that will hold each image on a separate page
            using (Document portfolio = new Document())
            {
                while (extractor.HasNextImage())
                {
                    // Retrieve the next image into a memory stream
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imgStream);
                        imgStream.Position = 0; // reset for reading

                        // Add a new page to the portfolio
                        Page page = portfolio.Pages.Add();

                        // Use the full page size for the image placement
                        // Aspose.Pdf.Rectangle constructor: (llx, lly, urx, ury)
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                            0,                                   // lower‑left X
                            0,                                   // lower‑left Y
                            page.Rect.URX,                       // upper‑right X (page width)
                            page.Rect.URY);                      // upper‑right Y (page height)

                        // Place the image on the page
                        page.AddImage(imgStream, rect);
                    }
                }

                // Save the resulting PDF portfolio
                portfolio.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Portfolio PDF created at '{outputPdfPath}'.");
    }
}