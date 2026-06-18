using System;
using System.IO;
using Aspose.Pdf;

class InsertHighResImage
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPdfPath = "output.pdf";
        const string highResImagePath = "high_res_image.jpg";

        // Ensure the image file exists
        if (!File.Exists(highResImagePath))
        {
            Console.Error.WriteLine($"Image not found: {highResImagePath}");
            return;
        }

        // Create a new PDF document and add a blank page
        using (Document pdfDoc = new Document())
        {
            pdfDoc.Pages.Add(); // first page is now available (index 1)

            // Define absolute coordinates for the image placement.
            // Rectangle constructor: (llx, lly, urx, ury)
            // Example places the image at (100, 500) with width 300 and height 200.
            Aspose.Pdf.Rectangle imageRect = new Aspose.Pdf.Rectangle(100, 500, 400, 700);

            // Open the image as a stream and add it to the page.
            using (FileStream imgStream = File.OpenRead(highResImagePath))
            {
                // AddImage(stream, rectangle) positions the image using absolute coordinates.
                pdfDoc.Pages[1].AddImage(imgStream, imageRect);
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"High‑resolution image inserted and saved to '{outputPdfPath}'.");
    }
}