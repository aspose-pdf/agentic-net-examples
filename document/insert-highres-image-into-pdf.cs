using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPdfPath = "output.pdf";
        const string highResImagePath = "highres.jpg";

        // Verify that the image file exists
        if (!File.Exists(highResImagePath))
        {
            Console.Error.WriteLine($"Image file not found: {highResImagePath}");
            return;
        }

        // Create a new PDF document (lifecycle: create)
        using (Document pdfDoc = new Document())
        {
            // Add a blank page to the document
            Page page = pdfDoc.Pages.Add();

            // Define the absolute position rectangle for the image
            // (llx, lly, urx, ury) – lower‑left and upper‑right corners
            Aspose.Pdf.Rectangle imageRect = new Aspose.Pdf.Rectangle(100, 500, 500, 800);

            // Open the image file as a stream and add it to the page
            using (FileStream imgStream = File.OpenRead(highResImagePath))
            {
                // AddImage(Stream, Rectangle) keeps the image proportion by default
                page.AddImage(imgStream, imageRect);
            }

            // Save the PDF document (lifecycle: save)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with high‑resolution image saved to '{outputPdfPath}'.");
    }
}