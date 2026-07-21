using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // ---------------------------------------------------------------------
        // 1. Create a minimal source PDF (if it does not already exist).
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // add a blank page
                doc.Save(inputPdfPath);
            }
        }

        // ---------------------------------------------------------------------
        // 2. Create an image in memory – no file is written to disk.
        // ---------------------------------------------------------------------
        using (MemoryStream imageStream = new MemoryStream())
        {
            // Example: a simple bitmap with some text.
            using (Bitmap bmp = new Bitmap(200, 100))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(System.Drawing.Color.LightBlue);
                    // Fully qualify System.Drawing.Font and FontStyle to avoid ambiguity.
                    using (System.Drawing.Font font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold))
                    {
                        g.DrawString("Stamp", font, System.Drawing.Brushes.DarkBlue, new PointF(20, 35));
                    }
                }
                // Save the bitmap as PNG into the memory stream.
                bmp.Save(imageStream, System.Drawing.Imaging.ImageFormat.Png);
            }
            // Reset the stream position before binding.
            imageStream.Position = 0;

            // -----------------------------------------------------------------
            // 3. Initialise the PdfFileStamp facade using the modern API.
            // -----------------------------------------------------------------
            using (PdfFileStamp pdfStamp = new PdfFileStamp())
            {
                pdfStamp.BindPdf(inputPdfPath);

                // -----------------------------------------------------------------
                // 4. Configure the image stamp.
                // -----------------------------------------------------------------
                // Fully qualify the Stamp type from Aspose.Pdf.Facades to avoid conflict with Aspose.Pdf.Stamp.
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                stamp.SetOrigin(100, 500);          // X = 100, Y = 500 (points)
                stamp.SetImageSize(200, 100);       // Width = 200, Height = 100 (points)
                stamp.Opacity = 0.8f;               // 80 % opacity
                stamp.IsBackground = false;        // place on top of page content
                stamp.BindImage(imageStream);       // bind directly from the stream

                // -----------------------------------------------------------------
                // 5. Apply the stamp and save the result.
                // -----------------------------------------------------------------
                pdfStamp.AddStamp(stamp);
                pdfStamp.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdfPath}'.");
    }
}
