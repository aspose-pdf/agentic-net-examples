using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "image.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the existing PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Select the page where the image will be placed (1‑based index)
            Page page = pdfDoc.Pages[1];

            // Define the rectangle for the image placement.
            // Lower‑left corner (x1, y1) = (100, 500)
            // Upper‑right corner (x2, y2) = (300, 650)  => width 200, height 150
            Aspose.Pdf.Rectangle imageRect = new Aspose.Pdf.Rectangle(100, 500, 300, 650);

            // Insert the raster image using the core API.
            page.AddImage(imagePath, imageRect);

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Raster image inserted and saved to '{outputPdfPath}'.");
    }
}