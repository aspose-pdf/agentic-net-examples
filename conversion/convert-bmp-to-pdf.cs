using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string bmpPath = "input.bmp";
        const string pdfPath = "output.pdf";

        if (!File.Exists(bmpPath))
        {
            Console.Error.WriteLine($"BMP file not found: {bmpPath}");
            return;
        }

        // Create a new PDF document; wrap in using for deterministic disposal
        using (Document pdfDoc = new Document())
        {
            // Add a blank page to the document
            Page page = pdfDoc.Pages.Add();

            // Load the BMP image via the Image.File property
            // Use fully qualified Aspose.Pdf.Image to avoid ambiguity with System.Drawing.Image
            Aspose.Pdf.Image img = new Aspose.Pdf.Image
            {
                File = bmpPath,
                // Optional: fit the image to the page size
                // Width = page.PageInfo.Width,
                // Height = page.PageInfo.Height
            };

            // Insert the image into the page's paragraph collection
            page.Paragraphs.Add(img);

            // Save the document as PDF (default format)
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"BMP successfully converted to PDF: {pdfPath}");
    }
}