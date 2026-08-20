using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the PDF files (the image will be generated in memory)
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // ---------------------------------------------------------------------
        // 1. Ensure a source PDF exists – create a minimal one‑page document.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add();
                placeholder.Save(inputPdfPath);
            }
        }

        // ---------------------------------------------------------------------
        // 2. Create a simple sample image in memory (e.g., a red rectangle).
        // ---------------------------------------------------------------------
        byte[] imageBytes;
        using (var bmp = new Bitmap(200, 100))
        {
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(System.Drawing.Color.Transparent);
                using (var brush = new SolidBrush(System.Drawing.Color.Red))
                {
                    g.FillRectangle(brush, 0, 0, bmp.Width, bmp.Height);
                }
            }
            using (var ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Png);
                imageBytes = ms.ToArray();
            }
        }

        // ---------------------------------------------------------------------
        // 3. Define the rectangle (in points) where the image stamp should fit.
        //    Rectangle(left, bottom, right, top)
        //    Example: place the stamp at (100, 500) with width 200 and height 100.
        // ---------------------------------------------------------------------
        double llx = 100; // left
        double lly = 500; // bottom
        double urx = llx + 200; // right
        double ury = lly + 100; // top
        Aspose.Pdf.Rectangle targetRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

        // ---------------------------------------------------------------------
        // 4. Load the PDF document, create the ImageStamp from the in‑memory image,
        //    configure it to fit exactly inside the target rectangle, and save.
        // ---------------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create ImageStamp from the generated image bytes
            using (var imgStream = new MemoryStream(imageBytes))
            {
                imgStream.Position = 0; // ensure the stream is at the beginning
                ImageStamp imgStamp = new ImageStamp(imgStream);

                // Configure the stamp size and position to match the rectangle
                imgStamp.Width   = targetRect.Width;   // stamp width
                imgStamp.Height  = targetRect.Height;  // stamp height
                imgStamp.XIndent = targetRect.LLX;     // lower‑left X coordinate
                imgStamp.YIndent = targetRect.LLY;     // lower‑left Y coordinate

                // Optional settings
                imgStamp.Background = false; // place on top of page content
                imgStamp.Opacity    = 1.0;    // fully opaque

                // Add the stamp to the first page (pages are 1‑based)
                pdfDoc.Pages[1].AddStamp(imgStamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdfPath}'.");
    }
}
