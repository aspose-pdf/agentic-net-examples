using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "portfolio.pdf";

        // ------------------------------------------------------------
        // 1. Create a minimal input PDF (self‑contained example).
        //    The PDF contains a single page with a simple generated image
        //    so that the extractor has something to work with.
        // ------------------------------------------------------------
        CreateSamplePdfWithImage(inputPdfPath);

        // ------------------------------------------------------------
        // 2. Extract all images from the source PDF into memory streams.
        // ------------------------------------------------------------
        List<MemoryStream> imageStreams = new List<MemoryStream>();
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage();

            while (extractor.HasNextImage())
            {
                MemoryStream imgStream = new MemoryStream();
                extractor.GetNextImage(imgStream);
                imgStream.Position = 0; // reset for later reading
                imageStreams.Add(imgStream);
            }
        }

        // ------------------------------------------------------------
        // 3. Create a new PDF document where each page contains one
        //    extracted image (PDF portfolio).
        // ------------------------------------------------------------
        using (Document portfolioDoc = new Document())
        {
            foreach (MemoryStream imgStream in imageStreams)
            {
                // Add a new blank page.
                Page page = portfolioDoc.Pages.Add();

                // Define a rectangle that covers the whole page.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    0,
                    0,
                    page.PageInfo.Width,
                    page.PageInfo.Height);

                // Add the image to the page – the image will be stretched to fill the page.
                page.AddImage(imgStream, rect);
            }

            // Save the resulting PDF portfolio.
            portfolioDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Portfolio PDF created: {outputPdfPath}");
    }

    /// <summary>
    /// Generates a one‑page PDF that contains a simple generated bitmap image.
    /// This method guarantees that the example runs in an isolated sandbox where
    /// no external files are present.
    /// </summary>
    private static void CreateSamplePdfWithImage(string path)
    {
        // Create a 100x100 red square bitmap in memory.
        using (Bitmap bmp = new Bitmap(100, 100))
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Fully qualify System.Drawing.Color to avoid ambiguity with Aspose.Pdf.Color.
                g.Clear(System.Drawing.Color.Red);
            }

            using (MemoryStream imgStream = new MemoryStream())
            {
                // Save bitmap as PNG to the stream.
                bmp.Save(imgStream, System.Drawing.Imaging.ImageFormat.Png);
                imgStream.Position = 0;

                // Build a PDF and place the image on the first page.
                using (Document doc = new Document())
                {
                    Page page = doc.Pages.Add();
                    // Define a rectangle where the image will be placed (centered).
                    double imgWidth = 200.0;
                    double imgHeight = 200.0;
                    double llx = (page.PageInfo.Width - imgWidth) / 2.0;
                    double lly = (page.PageInfo.Height - imgHeight) / 2.0;
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, llx + imgWidth, lly + imgHeight);

                    page.AddImage(imgStream, rect);
                    doc.Save(path);
                }
            }
        }
    }
}
