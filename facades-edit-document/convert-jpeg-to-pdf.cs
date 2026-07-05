using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Facades;      // Facades namespace (required by task)

class Program
{
    static void Main()
    {
        const string imagePath  = "Photo.jpg";   // Input JPEG image
        const string pdfPath    = "Photo.pdf";   // Output single‑page PDF

        // Verify the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document and ensure deterministic disposal
        using (Document pdfDoc = new Document())
        {
            // Add a blank page (default size, default margins)
            pdfDoc.Pages.Add();

            // Create an Image object, set its source file, and add it to the page
            Image img = new Image
            {
                File = imagePath
            };
            pdfDoc.Pages[1].Paragraphs.Add(img);

            // Save the PDF document to the specified path
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"JPEG image '{imagePath}' successfully converted to PDF '{pdfPath}'.");
    }
}