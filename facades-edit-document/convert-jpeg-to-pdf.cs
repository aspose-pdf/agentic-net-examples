using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as per requirement, though not directly used

class Program
{
    static void Main()
    {
        // Input JPEG image and output PDF file names
        const string imagePath = "Photo.jpg";
        const string outputPdf = "Photo.pdf";

        // Verify that the source image exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document and ensure it is disposed properly
        using (Document pdfDoc = new Document())
        {
            // Add a single page; default margins are applied automatically
            Page page = pdfDoc.Pages.Add();

            // Create an Aspose.Pdf.Image object and bind it to the JPEG file
            Image img = new Image
            {
                File = imagePath
            };

            // Insert the image into the page's content stream
            page.Paragraphs.Add(img);

            // Save the resulting PDF document
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF successfully created: {outputPdf}");
    }
}