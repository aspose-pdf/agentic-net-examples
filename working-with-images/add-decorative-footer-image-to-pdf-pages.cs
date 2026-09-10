using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string outputPdfPath  = "output.pdf";         // result PDF
        const string footerImagePath = "footer.png";        // decorative footer image

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

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Determine the page width; height of the footer rectangle can be fixed.
                double pageWidth = page.PageInfo.Width;
                double footerHeight = 50; // desired height of the footer area (points)

                // Define a rectangle that spans the full width at the bottom of the page.
                // Rectangle(left, bottom, right, top)
                Aspose.Pdf.Rectangle footerRect = new Aspose.Pdf.Rectangle(
                    0,                     // left
                    0,                     // bottom
                    pageWidth,             // right (full page width)
                    footerHeight);         // top

                // Add the image; autoAdjustRectangle = true (default) keeps the image proportion.
                using (FileStream imgStream = File.OpenRead(footerImagePath))
                {
                    page.AddImage(imgStream, footerRect);
                }
            }

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Decorative footer added to each page. Saved as '{outputPdfPath}'.");
    }
}