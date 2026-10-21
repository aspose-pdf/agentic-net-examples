using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string imagePath = "Photo.jpg";
        const string pdfPath   = "Photo.pdf";

        // Verify the source image exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document
        Document pdfDocument = new Document();
        // Add a page (default size and margins are applied automatically)
        Page page = pdfDocument.Pages.Add();

        // Create an image object and bind the JPEG file
        Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
        {
            File = imagePath,
            // Fit the image to the page while preserving margins (optional)
            // Width = page.PageInfo.Width - page.PageInfo.Margin.Left - page.PageInfo.Margin.Right,
            // Height = page.PageInfo.Height - page.PageInfo.Margin.Top - page.PageInfo.Margin.Bottom,
            // HorizontalAlignment = HorizontalAlignment.Center,
            // VerticalAlignment = VerticalAlignment.Center
        };

        // Add the image to the page's paragraphs collection
        page.Paragraphs.Add(pdfImage);

        // Save the generated PDF
        pdfDocument.Save(pdfPath);

        Console.WriteLine($"Successfully created PDF: {pdfPath}");
    }
}
