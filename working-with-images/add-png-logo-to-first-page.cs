using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF (will be created if missing)
        const string logoPng = "logo.png";    // PNG logo to add (will be created if missing)
        const string outputPdf = "output.pdf"; // result PDF

        // ------------------------------------------------------------
        // Ensure the source PDF exists – create a simple one if not.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            using (Document tempDoc = new Document())
            {
                // Add a blank page so we have something to work with.
                tempDoc.Pages.Add();
                tempDoc.Save(inputPdf);
            }
        }

        // ------------------------------------------------------------
        // Ensure the logo image exists – create a placeholder PNG if not.
        // ------------------------------------------------------------
        if (!File.Exists(logoPng))
        {
            using (Bitmap bmp = new Bitmap(100, 50))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(System.Drawing.Color.LightGray);
                    using (Pen pen = new Pen(System.Drawing.Color.DarkBlue, 2))
                    {
                        g.DrawRectangle(pen, 0, 0, 99, 49);
                    }
                    using (Brush brush = new SolidBrush(System.Drawing.Color.Black))
                    {
                        g.DrawString("Logo", new Font("Arial", 12), brush, new PointF(10, 15));
                    }
                }
                bmp.Save(logoPng, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        // Define the position and size of the logo.
        // Rectangle(left, bottom, right, top) – coordinates are in points.
        // Example places a 100x50 logo at (50,750) on the page.
        Aspose.Pdf.Rectangle logoRect = new Aspose.Pdf.Rectangle(50, 750, 150, 800);

        // Load the PDF, modify, and save – all within a using block for proper disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least one page.
            if (doc.Pages.Count < 1)
                throw new InvalidOperationException("The PDF does not contain any pages.");

            // Add the PNG image to the first page at the specified rectangle.
            doc.Pages[1].AddImage(logoPng, logoRect);

            // Save the modified document as PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Logo added and saved to '{outputPdf}'.");
    }
}
