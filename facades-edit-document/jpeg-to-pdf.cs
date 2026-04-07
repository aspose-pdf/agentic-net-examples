using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string imagePath = "Photo.jpg";
        const string outputPath = "photo.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        using (Document doc = new Document())
        {
            // Add a new page with default size and margins
            Page page = doc.Pages.Add();

            // Create an Image object and assign the JPEG file
            Image img = new Image();
            img.File = imagePath;

            // Place the image on the page
            page.Paragraphs.Add(img);

            // Save the single‑page PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created: {outputPath}");
    }
}