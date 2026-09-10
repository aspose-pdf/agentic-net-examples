using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class ResizeEmbeddedImage
{
    static void Main()
    {
        // Input PDF containing the raster image to be resized
        const string inputPdf = "input.pdf";
        // Output PDF with the resized image
        const string outputPdf = "output_resized.pdf";
        // Path to the source image file (the same image that is already embedded)
        const string imagePath = "image.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Target page – here we use the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Desired dimensions for the image (in points; 1 point = 1/72 inch)
            double newWidth  = 200; // e.g., 200 points (~2.78 inches)
            double newHeight = 150; // e.g., 150 points (~2.08 inches)

            // Position where the resized image will be placed.
            // llx, lly define the lower‑left corner; urx, ury define the upper‑right corner.
            double llx = 100; // left coordinate
            double lly = 500; // bottom coordinate
            double urx = llx + newWidth;
            double ury = lly + newHeight;

            // Fully qualified rectangle to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Add the image to the page with the specified rectangle.
            // The overload AddImage(Stream, Rectangle) respects the rectangle size.
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                page.AddImage(imgStream, rect);
            }

            // Save the modified PDF. The Document.Save(string) method always writes PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Resized image saved to '{outputPdf}'.");
    }
}