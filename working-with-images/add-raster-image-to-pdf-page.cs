using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string imagePath = "photo.jpg";
        const string outputPdf = "output.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new blank page to the document
            Page page = doc.Pages.Add();

            // Create an Image object and load the raster image from file
            Image img = new Image();
            // Use a FileStream to avoid locking the file after the operation
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                img.ImageStream = imgStream;
                // Optionally set dimensions (in points). 1 point = 1/72 inch.
                img.FixWidth = 300;   // width in points
                img.FixHeight = 200;  // height in points
            }

            // Add the image to the page's Paragraphs collection
            page.Paragraphs.Add(img);

            // Save the PDF document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with image saved to '{outputPdf}'.");
    }
}