using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath    = "input.pdf";   // existing PDF
        const string imagePath  = "image.png";   // image to insert
        const string outputPath = "output.pdf";  // result PDF

        // Validate input files
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Select the page where the image will be placed (first page in this example)
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the rectangle that represents the image position.
            // (llx, lly, urx, ury) in points (1 point = 1/72 inch).
            // The rectangle can be sized as needed; AddImage will keep the original
            // image resolution and aspect ratio (autoAdjustRectangle = true by default).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 650);

            // Insert the image from file path. The method preserves the image's original
            // resolution and scales it proportionally within the rectangle.
            page.AddImage(imagePath, rect);

            // Save the modified document as PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image inserted and saved to '{outputPath}'.");
    }
}