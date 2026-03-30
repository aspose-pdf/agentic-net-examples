using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input file not found: " + inputPdf);
            return;
        }

        List<string> extractedImages = new List<string>();

        // Extract images from the source PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage();

            int imageCounter = 1;
            while (extractor.HasNextImage())
            {
                string imageFile = $"image-{imageCounter}.png";
                extractor.GetNextImage(imageFile);
                extractedImages.Add(imageFile);
                imageCounter++;
            }
        }

        // Create a new PDF/A‑2b document and embed the extracted images as XObjects
        using (Document pdfaDoc = new Document())
        {
            foreach (string imgPath in extractedImages)
            {
                if (!File.Exists(imgPath))
                    continue;

                // Add a new page for each image
                Page page = pdfaDoc.Pages.Add();

                using (FileStream imgStream = File.OpenRead(imgPath))
                {
                    // Add the image to the page resources and obtain its name (string)
                    string imageName = page.Resources.Images.Add(imgStream);

                    // Define the rectangle that covers the whole page
                    var rect = new Aspose.Pdf.Rectangle(0, 0, page.PageInfo.Width, page.PageInfo.Height);

                    // Embed the image on the page using the resource name (string overload)
                    page.AddImage(imageName, rect);
                }
            }

            // Convert the document to PDF/A‑2b compliance
            pdfaDoc.Convert("conversion_log.xml", PdfFormat.PDF_A_2B, ConvertErrorAction.Delete);
            pdfaDoc.Save(outputPdf);
        }

        // Cleanup temporary image files
        foreach (string imgPath in extractedImages)
        {
            try
            {
                File.Delete(imgPath);
            }
            catch
            {
                // ignore any errors during cleanup
            }
        }

        Console.WriteLine("PDF/A-2b document created: " + outputPdf);
    }
}
