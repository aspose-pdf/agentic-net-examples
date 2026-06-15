using System;
using System.IO;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        // Create a simple PNG image to use as stamp
        string imagePath = "stamp.png";
        byte[] pngBytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK6cAAAAASUVORK5CYII=");
        File.WriteAllBytes(imagePath, pngBytes);

        // Create a sample PDF with four pages
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Open the PDF and add a FloatingBox with background image on page 4
        using (Document doc = new Document("input.pdf"))
        {
            Page page = doc.Pages[4];

            FloatingBox floatingBox = new FloatingBox((float)page.PageInfo.Width, (float)page.PageInfo.Height);
            Image backgroundImage = new Image();
            backgroundImage.File = imagePath;
            floatingBox.BackgroundImage = backgroundImage;
            floatingBox.ZIndex = -1;

            page.Paragraphs.Add(floatingBox);
            doc.Save("output.pdf");
        }
    }
}