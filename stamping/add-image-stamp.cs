using System;
using System.IO;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with two pages
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Create a simple PNG image for stamping (a red square)
        string base64Image = "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAFklEQVQoU2NkYGD4z0AEYBxVSFIAABcAAf+XKXcAAAAASUVORK5CYII=";
        byte[] imageBytes = Convert.FromBase64String(base64Image);
        File.WriteAllBytes("stamp.png", imageBytes);

        // Open the PDF and add the image stamp to page 2
        using (Document pdfDoc = new Document("input.pdf"))
        {
            ImageStamp stamp = new ImageStamp("stamp.png");
            stamp.Quality = 100;
            stamp.Opacity = 0.8;
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment = VerticalAlignment.Center;

            pdfDoc.Pages[2].AddStamp(stamp);
            pdfDoc.Save("output.pdf");
        }
    }
}