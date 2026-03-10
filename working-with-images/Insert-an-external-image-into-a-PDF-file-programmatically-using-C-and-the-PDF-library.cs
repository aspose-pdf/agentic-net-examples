using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the external image file
        const string imagePath = "image.png";

        // Path for the resulting PDF
        const string outputPdf = "output.pdf";

        // Verify that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create an empty PDF document and add a single page
        using (Document pdfDocument = new Document())
        {
            pdfDocument.Pages.Add(); // 1‑based indexing

            // Create an Image object (parameterless constructor) and assign the file
            Image img = new Image
            {
                File = imagePath
            };

            // Insert the image into the first page's paragraph collection
            pdfDocument.Pages[1].Paragraphs.Add(img);

            // Save the PDF document
            pdfDocument.Save(outputPdf);
        }

        Console.WriteLine($"PDF with inserted image saved to '{outputPdf}'.");
    }
}