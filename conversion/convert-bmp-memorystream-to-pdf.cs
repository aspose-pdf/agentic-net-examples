using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input BMP file path and output PDF path
        const string bmpPath = "image.bmp";
        const string outputPdf = "output.pdf";

        if (!File.Exists(bmpPath))
        {
            Console.Error.WriteLine($"BMP file not found: {bmpPath}");
            return;
        }

        // Load BMP into a MemoryStream
        using (FileStream bmpFile = File.OpenRead(bmpPath))
        using (MemoryStream ms = new MemoryStream())
        {
            bmpFile.CopyTo(ms);
            ms.Position = 0; // Ensure stream is at the beginning

            // Create a new PDF document
            using (Document pdfDoc = new Document())
            {
                // Add a page to the document
                Page page = pdfDoc.Pages.Add();

                // Create an Image object and assign the MemoryStream
                Image img = new Image
                {
                    ImageStream = ms
                };

                // Add the image to the page's content
                page.Paragraphs.Add(img);

                // Save the PDF document
                pdfDoc.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF created successfully: {outputPdf}");
    }
}