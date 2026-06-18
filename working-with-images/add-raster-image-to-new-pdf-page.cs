using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, Page, Image, etc.

class Program
{
    static void Main()
    {
        // Input image file (any raster format: PNG, JPEG, BMP, GIF, etc.)
        const string imagePath = "sample_image.png";

        // Output PDF file that will contain the image on a new page.
        const string pdfPath = "image_page.pdf";

        // Verify that the image file exists before proceeding.
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Create a new empty PDF document.
            using (Document pdfDoc = new Document())
            {
                // Add a blank page to the document (pages are 1‑based).
                pdfDoc.Pages.Add();

                // Create an Image object (derived from BaseParagraph).
                Image img = new Image();

                // Assign the image file path. The Image class will load the raster data internally.
                img.File = imagePath;

                // Add the Image to the Paragraphs collection of the first page.
                // This places the image on the page using its natural size and alignment.
                pdfDoc.Pages[1].Paragraphs.Add(img);

                // Save the PDF document to the specified file.
                pdfDoc.Save(pdfPath);
            }

            Console.WriteLine($"PDF with image saved to '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}