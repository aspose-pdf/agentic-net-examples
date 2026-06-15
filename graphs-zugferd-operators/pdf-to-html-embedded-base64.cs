using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PNG image (1x1 pixel) and save to file
        string imagePath = "sample.png";
        byte[] pngBytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+X2ZcAAAAASUVORK5CYII=");
        File.WriteAllBytes(imagePath, pngBytes);

        // Create a sample PDF with an image and some text
        using (Document pdfDocument = new Document())
        {
            // Add a page
            Page page = pdfDocument.Pages.Add();

            // Add text
            TextFragment text = new TextFragment("Sample PDF with embedded image");
            text.Position = new Position(100, 700);
            page.Paragraphs.Add(text);

            // Add image
            Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();
            pdfImage.File = imagePath;
            pdfImage.FixWidth = 100;
            pdfImage.FixHeight = 100;
            pdfImage.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;
            page.Paragraphs.Add(pdfImage);

            // Save the PDF
            pdfDocument.Save("sample.pdf");
        }

        // Load the PDF and convert to HTML with images embedded as Base64 data URIs
        using (Document pdfDocument = new Document("sample.pdf"))
        {
            HtmlSaveOptions saveOptions = new HtmlSaveOptions();
            saveOptions.RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg;

            pdfDocument.Save("output.html", saveOptions);
        }

        // Optional: clean up temporary image file
        // File.Delete(imagePath);
    }
}
