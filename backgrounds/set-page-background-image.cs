using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string imagePath = "background.jpg";
        const string outputPdf = "output.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document with deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Load the image and set it as the page background
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                Image bgImage = new Image
                {
                    ImageStream = imgStream
                };
                page.BackgroundImage = bgImage;
            }

            // Save the PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}