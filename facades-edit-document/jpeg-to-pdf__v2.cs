using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string imagePath = "Photo.jpg";
        const string outputPath = "output.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        using (Document pdfDocument = new Document())
        {
            // Add a new page with default size and margins
            Page page = pdfDocument.Pages.Add();

            // Create an Image object and assign the JPEG file
            Image image = new Image();
            image.File = imagePath;

            // Place the image on the page
            page.Paragraphs.Add(image);

            // Save the PDF document
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"PDF created: {outputPath}");
    }
}