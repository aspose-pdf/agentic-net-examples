using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a simple logo image file (1x1 pixel PNG)
        string logoPath = "logo.png";
        byte[] logoBytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XcZcAAAAASUVORK5CYII=");
        using (FileStream logoStream = new FileStream(logoPath, FileMode.Create, FileAccess.Write))
        {
            logoStream.Write(logoBytes, 0, logoBytes.Length);
        }

        // Create a sample PDF document
        string inputPath = "input.pdf";
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save(inputPath);
        }

        // Open the PDF and add a semi‑transparent logo stamp to the first page
        using (Document doc = new Document(inputPath))
        {
            ImageStamp logoStamp = new ImageStamp(logoPath);
            logoStamp.Opacity = 0.5f;
            logoStamp.HorizontalAlignment = HorizontalAlignment.Center;
            logoStamp.VerticalAlignment = VerticalAlignment.Center;
            // Add stamp to the first page (1‑based index)
            doc.Pages[1].AddStamp(logoStamp);
            doc.Save("output.pdf");
        }
    }
}