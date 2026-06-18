using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.PDF API

class Program
{
    static void Main()
    {
        // Input JPEG image and desired PDF output file names
        const string imagePath = "Photo.jpg";
        const string pdfPath   = "Photo.pdf";

        // Verify that the source image exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document with a single page
        using (var pdfDocument = new Document())
        {
            // Add a page (default size and default margins are used)
            var page = pdfDocument.Pages.Add();

            // Load the JPEG image into a stream
            using (var imageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                // Create an Aspose.Pdf.Image object and assign the stream
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = imageStream,
                    // Fit the image to the page's default margins
                    // (Width/Height are automatically scaled to fit if larger than the page)
                };

                // Add the image to the page's paragraphs collection
                page.Paragraphs.Add(pdfImage);
            }

            // Save the PDF document to the specified path
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF created successfully: {pdfPath}");
    }
}
