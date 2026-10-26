using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string imagePath = "photo.jpg";
        const string outputPdf = "output.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Load the raster image from a file stream
            using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                Image img = new Image { ImageStream = imgStream };

                // Optional: scale the image to fit the page
                // img.FixWidth = page.PageInfo.Width;
                // img.FixHeight = page.PageInfo.Height;

                // Insert the image into the page via the Paragraphs collection
                page.Paragraphs.Add(img);
            }

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with image saved to '{outputPdf}'.");
    }
}
