using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string imagePath = "background.jpg";
        const string outputPath = "result.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        using (var doc = new Document())
        {
            // Add a new blank page
            Page page = doc.Pages.Add();

            // Load the background image from a stream and assign it to the page
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                Image bgImage = new Image { ImageStream = imgStream };
                page.BackgroundImage = bgImage;
            }

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
