using System;
using System.IO;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        // Create a simple PNG image (1x1 red pixel) for stamping
        byte[] pngBytes = Convert.FromBase64String(
            "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/5+BAQAE/wJ/6V5ZAAAAAElFTkSuQmCC");
        string imagePath = "stamp.png";
        File.WriteAllBytes(imagePath, pngBytes);

        // Create a sample PDF with one page
        using (Document pdfDocument = new Document())
        {
            pdfDocument.Pages.Add();
            pdfDocument.Save("input.pdf");
        }

        // Open the PDF and add the image stamp aligned to top‑right with margins
        using (Document pdfDocument = new Document("input.pdf"))
        {
            Page page = pdfDocument.Pages[1];

            ImageStamp stamp = new ImageStamp(imagePath);
            stamp.HorizontalAlignment = HorizontalAlignment.Right;
            stamp.VerticalAlignment = VerticalAlignment.Top;
            stamp.RightMargin = 20; // points from the right edge
            stamp.TopMargin = 20;   // points from the top edge

            page.AddStamp(stamp);
            pdfDocument.Save("output.pdf");
        }
    }
}