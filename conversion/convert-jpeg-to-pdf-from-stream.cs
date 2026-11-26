using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string jpgPath   = "input.jpg";   // path to the source JPEG image
        const string pdfPath   = "output.pdf";  // path for the generated PDF

        if (!File.Exists(jpgPath))
        {
            Console.Error.WriteLine($"Image file not found: {jpgPath}");
            return;
        }

        // Load the JPEG image from a stream and embed it into a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a blank page to the document
            Page page = pdfDoc.Pages.Add();

            // Open the JPEG file as a stream
            using (FileStream imgStream = File.OpenRead(jpgPath))
            {
                // Create an Image object and assign the stream
                Image img = new Image
                {
                    ImageStream = imgStream
                };

                // Add the image to the page's paragraph collection
                page.Paragraphs.Add(img);
            }

            // Save the PDF; Document.Save(string) writes PDF regardless of extension
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"JPEG image converted to PDF: {pdfPath}");
    }
}