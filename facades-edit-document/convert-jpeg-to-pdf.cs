using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API

class Program
{
    static void Main()
    {
        const string inputImagePath = "Photo.jpg"; // JPEG source
        const string outputPdfPath = "Photo.pdf";   // Resulting PDF

        // Verify the JPEG file exists
        if (!File.Exists(inputImagePath))
        {
            Console.Error.WriteLine($"Input image not found: {inputImagePath}");
            return;
        }

        // Create a new PDF document
        Document pdfDocument = new Document();
        // Add a blank page (default margins are applied automatically)
        Page page = pdfDocument.Pages.Add();

        // Load the JPEG image and add it to the page
        using (FileStream imageStream = new FileStream(inputImagePath, FileMode.Open, FileAccess.Read))
        {
            Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
            {
                ImageStream = imageStream
            };
            // The image is added as a paragraph; it will be placed respecting the page margins.
            page.Paragraphs.Add(pdfImage);
        }

        // Save the PDF document
        pdfDocument.Save(outputPdfPath);

        Console.WriteLine($"PDF created successfully at '{outputPdfPath}'.");
    }
}
