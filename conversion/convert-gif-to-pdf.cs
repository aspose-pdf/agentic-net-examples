using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string gifPath = "image.gif";
        const string pdfPath = "output.pdf";

        // Verify the source GIF exists
        if (!File.Exists(gifPath))
        {
            Console.Error.WriteLine($"GIF file not found: {gifPath}");
            return;
        }

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Load the GIF image via the Image.File property
            Image img = new Image
            {
                File = gifPath
            };

            // Insert the image into the page's content
            page.Paragraphs.Add(img);

            // Save the document as PDF (no SaveOptions needed for PDF output)
            doc.Save(pdfPath);
        }

        Console.WriteLine($"GIF successfully converted to PDF: {pdfPath}");
    }
}