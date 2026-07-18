using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string footerImagePath = "footer.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(footerImagePath))
        {
            Console.Error.WriteLine($"Footer image not found: {footerImagePath}");
            return;
        }

        // Load the source PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // Determine the width of the page (media box)
                double pageWidth = page.Rect.Width;

                // Desired height of the footer image after scaling (adjust as needed)
                double footerHeight = 50; // points

                // Define the rectangle where the footer image will be placed (bottom of the page)
                Aspose.Pdf.Rectangle footerRect = new Aspose.Pdf.Rectangle(
                    0,                     // left
                    0,                     // bottom
                    pageWidth,             // right (full page width)
                    footerHeight);         // top

                // Add the image to the page within the defined rectangle.
                // The image will be proportionally scaled to fit the rectangle.
                using (FileStream imgStream = File.OpenRead(footerImagePath))
                {
                    page.AddImage(imgStream, footerRect);
                }
            }

            // Save the modified PDF (PDF output, no SaveOptions needed)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with image footer saved to '{outputPdfPath}'.");
    }
}