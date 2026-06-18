using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create a tiny PNG image (1x1 pixel, red) and save it to disk
        string imagePath = "sample-image.png";
        byte[] pngBytes = Convert.FromBase64String(
            "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK6cAAAAASUVORK5CYII=");
        File.WriteAllBytes(imagePath, pngBytes);

        // Create a sample PDF that contains the image
        string pdfPath = "sample.pdf";
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();
            pdfImage.File = imagePath;
            page.Paragraphs.Add(pdfImage);
            doc.Save(pdfPath);
        }

        // Extract images from the PDF
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(pdfPath);
        extractor.ExtractImage();

        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            string extractedImagePath = "extracted-image-" + imageIndex + ".png";
            extractor.GetNextImage(extractedImagePath);
            // At this point you could embed 'extractedImagePath' into a Word document
            // using the Open XML SDK (not included in this example).
            imageIndex++;
        }
    }
}