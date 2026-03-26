using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string imagePath = "highres.jpg";
        const string outputPath = "output.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        using (Document pdfDocument = new Document())
        {
            // Add a blank page to the document
            Page page = pdfDocument.Pages.Add();

            // Define the rectangle (absolute coordinates) where the image will be placed
            // Lower‑left corner (100, 400), upper‑right corner (500, 800)
            Aspose.Pdf.Rectangle imageRect = new Aspose.Pdf.Rectangle(100.0, 400.0, 500.0, 800.0);

            using (FileStream imageStream = File.OpenRead(imagePath))
            {
                // Insert the high‑resolution image at the specified position
                page.AddImage(imageStream, imageRect);
            }

            // Save the resulting PDF
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Image inserted and saved to '{outputPath}'.");
    }
}