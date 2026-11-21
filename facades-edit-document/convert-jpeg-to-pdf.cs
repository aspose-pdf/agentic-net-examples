using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string imagePath = "Photo.jpg";
        const string outputPdf = "Photo.pdf";

        // Verify that the source image exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create a new PDF document with default page size and margins
        Document pdfDocument = new Document();
        // Add a page to the document (default size A4)
        Page page = pdfDocument.Pages.Add();

        // Create an Image object and set its source file
        Image pdfImage = new Image
        {
            File = imagePath
        };

        // Add the image to the page's paragraphs collection – it will be placed
        // using the default margins of the page.
        page.Paragraphs.Add(pdfImage);

        // Save the PDF document to the specified output path
        pdfDocument.Save(outputPdf);

        Console.WriteLine($"Successfully created PDF: {outputPdf}");
    }
}
