using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string tiffPath = "image.tif";
        const string pdfPath  = "output.pdf";

        if (!File.Exists(tiffPath))
        {
            Console.Error.WriteLine($"File not found: {tiffPath}");
            return;
        }

        // Document must be disposed deterministically
        using (Document pdfDoc = new Document())
        {
            // Add a new page to the PDF
            Page page = pdfDoc.Pages.Add();

            // Load the TIFF via the Image.File property
            Aspose.Pdf.Image img = new Aspose.Pdf.Image
            {
                File = tiffPath
            };

            // Optionally scale the image to fill the page
            img.FixWidth  = page.Rect.Width;
            img.FixHeight = page.Rect.Height;

            // Place the image on the page
            page.Paragraphs.Add(img);

            // Save as PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"TIFF converted to PDF: {pdfPath}");
    }
}