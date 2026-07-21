using System;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the PDF and PNG files used in the demo
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string pngImagePath  = "image.png";

        // ---------------------------------------------------------------------
        // 1. Create a minimal source PDF (2 pages) if it does not already exist.
        // ---------------------------------------------------------------------
        if (!System.IO.File.Exists(inputPdfPath))
        {
            using (Document seed = new Document())
            {
                // Add two blank pages – page 2 is required for the image insertion.
                seed.Pages.Add();
                seed.Pages.Add();
                seed.Save(inputPdfPath);
            }
        }

        // ---------------------------------------------------------------------
        // 2. Create a simple PNG image if it does not already exist.
        // ---------------------------------------------------------------------
        if (!System.IO.File.Exists(pngImagePath))
        {
            using (Bitmap bmp = new Bitmap(100, 100))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(System.Drawing.Color.Transparent);
                    using (Pen pen = new Pen(System.Drawing.Color.Blue, 5))
                    {
                        g.DrawEllipse(pen, 10, 10, 80, 80);
                    }
                }
                bmp.Save(pngImagePath, ImageFormat.Png);
            }
        }

        // Coordinates for the image rectangle on page 2 (lower‑left X/Y, upper‑right X/Y)
        const float lowerLeftX  = 100f;
        const float lowerLeftY  = 200f;
        const float upperRightX = 300f;
        const float upperRightY = 400f;

        // ---------------------------------------------------------------------
        // 3. Bind the PDF, add the image, and save the result.
        // ---------------------------------------------------------------------
        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(inputPdfPath);
        // Page numbers are 1‑based; we target page 2.
        mend.AddImage(pngImagePath, 2, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
        mend.Save(outputPdfPath);
        mend.Close();

        Console.WriteLine($"Image added to page 2 and saved as '{outputPdfPath}'.");
    }
}
