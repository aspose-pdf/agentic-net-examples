using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string imagePath = "Photo.jpg";
        const string outputPdf = "Photo.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document.
        using (Document pdfDocument = new Document())
        {
            // Add a page with default margins.
            Page page = pdfDocument.Pages.Add();

            // Load the JPEG image.
            Image image = new Image
            {
                ImageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read)
            };

            // Add the image to the page. The image will be placed at the origin (0,0) and will keep its original size.
            // If you need it centered or scaled, adjust the image's properties accordingly.
            page.Paragraphs.Add(image);

            // Save the PDF.
            pdfDocument.Save(outputPdf);
        }

        Console.WriteLine($"PDF created successfully: {outputPdf}");
    }
}
