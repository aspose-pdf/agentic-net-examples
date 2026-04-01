using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPdf = "output.pdf";
        const string imagesFolder = "images";

        // Ensure the images folder exists
        if (!Directory.Exists(imagesFolder))
        {
            Console.Error.WriteLine($"Images folder not found: {imagesFolder}");
            return;
        }

        // Create a new PDF document with a single blank page
        using (Document pdfDocument = new Document())
        {
            Page page = pdfDocument.Pages.Add();

            // Define the rectangle where each image will be placed
            Aspose.Pdf.Rectangle imageRect = new Aspose.Pdf.Rectangle(50, 500, 300, 700);

            // Iterate over all image files in the folder
            foreach (string imagePath in Directory.GetFiles(imagesFolder))
            {
                try
                {
                    // Attempt to add the image to the page
                    page.AddImage(imagePath, imageRect);
                    Console.WriteLine($"Added image: {imagePath}");
                }
                catch (Exception ex)
                {
                    // Log the problematic image file path and the exception message
                    Console.Error.WriteLine($"Failed to add image '{imagePath}': {ex.Message}");
                }
            }

            // Save the resulting PDF
            pdfDocument.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}