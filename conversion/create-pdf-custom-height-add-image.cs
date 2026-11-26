using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string imagePath = "banner.jpg";
        const string outputPath = "height700.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Set the page height to 700 points (width remains default)
            page.PageInfo.Height = 700;

            // Add the banner image to the page
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // The Image class does not have an ImageInfo property; set the stream directly.
                Image img = new Image { ImageStream = imgStream };
                page.Paragraphs.Add(img);
            }

            // Save the PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
