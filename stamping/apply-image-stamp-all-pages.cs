using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a sample PDF with two pages
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Create a simple PNG image to use as a stamp
        string stampPath = "stamp.png";
        string base64Png = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK8cAAAAASUVORK5CYII=";
        byte[] imageBytes = Convert.FromBase64String(base64Png);
        File.WriteAllBytes(stampPath, imageBytes);

        // Open the PDF and apply the image stamp to each page
        using (Document pdfDoc = new Document("input.pdf"))
        {
            foreach (Page page in pdfDoc.Pages)
            {
                ImageStamp stamp = new ImageStamp(stampPath);
                stamp.Width = 50;
                stamp.Height = 50;
                stamp.HorizontalAlignment = HorizontalAlignment.Right;
                stamp.VerticalAlignment = VerticalAlignment.Bottom;
                page.AddStamp(stamp);
            }
            pdfDoc.Save("output.pdf");
        }
    }
}
