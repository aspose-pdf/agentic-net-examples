using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string footerImagePath = "footer.png";
        const string outputPdfPath  = "output.pdf";

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

        // Load the source PDF
        using (Document doc = new Document(inputPdfPath))
        {
            // Load the image once to obtain its original dimensions
            Aspose.Pdf.Image sampleImg = new Aspose.Pdf.Image();
            sampleImg.File = footerImagePath;
            double imgOrigWidth  = sampleImg.BitmapSize.Width;
            double imgOrigHeight = sampleImg.BitmapSize.Height;

            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Page width (points)
                double pageWidth = page.PageInfo.Width;

                // Compute scaling factor so the image spans the full page width
                double scale = pageWidth / imgOrigWidth;
                double scaledHeight = imgOrigHeight * scale;

                // Define rectangle at the bottom of the page
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    0,                     // llx
                    0,                     // lly
                    pageWidth,             // urx
                    scaledHeight           // ury
                );

                // Add the image; the stream is disposed after the call
                using (FileStream imgStream = File.OpenRead(footerImagePath))
                {
                    page.AddImage(imgStream, rect);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Decorative footer added to each page. Saved as '{outputPdfPath}'.");
    }
}