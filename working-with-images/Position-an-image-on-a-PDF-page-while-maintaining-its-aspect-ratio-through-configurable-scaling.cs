using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF
        const string outputPdfPath = "output.pdf";     // result PDF
        const string imagePath     = "image.png";      // image to place
        const double scaleFactor   = 0.4;              // 40 % of page width (configurable)

        if (!File.Exists(inputPdfPath) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Choose the page where the image will be placed (1‑based indexing)
            Page page = pdfDoc.Pages[1];

            // Determine the rectangle that will contain the image.
            // The rectangle is centered on the page and its width is a fraction
            // of the page width defined by scaleFactor. Height is calculated
            // automatically by AddImage (autoAdjustRectangle = true) to keep
            // the image's original aspect ratio.
            double pageWidth  = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            double rectWidth  = pageWidth * scaleFactor;
            double llx = (pageWidth  - rectWidth) / 2;          // left‑bottom X
            double lly = (pageHeight - rectWidth) / 2;          // left‑bottom Y (temporary, will be adjusted)
            double urx = llx + rectWidth;                       // upper‑right X
            double ury = lly + rectWidth;                       // upper‑right Y (temporary)

            // Fully qualified rectangle to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle imageRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Add the image stream to the page.
            // The overload with (Stream, Rectangle) keeps the image proportion
            // and centers it inside the supplied rectangle.
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                page.AddImage(imgStream, imageRect);
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image placed and saved to '{outputPdfPath}'.");
    }
}