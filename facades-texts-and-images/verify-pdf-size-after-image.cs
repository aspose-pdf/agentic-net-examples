using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfImageSizeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a simple sample PDF (self‑contained example)
            using (Document sampleDoc = new Document())
            {
                // Add a blank page (page indexing is 1‑based)
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Step 2: Create a temporary image file to be added
            string imagePath = "sample.jpg";
            using (Bitmap bmp = new Bitmap(100, 100))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // Use fully qualified System.Drawing.Color to avoid ambiguity with Aspose.Pdf.Color
                    g.Clear(System.Drawing.Color.Blue);
                }
                bmp.Save(imagePath);
            }

            // Step 3: Record original PDF size (in bytes)
            byte[] originalBytes = File.ReadAllBytes("input.pdf");
            long originalSize = originalBytes.Length;

            // Step 4: Add the image to the PDF using PdfFileMend
            using (PdfFileMend mend = new PdfFileMend())
            {
                mend.BindPdf("input.pdf");
                // Add image to page 1 at coordinates (100,100) – (200,200)
                mend.AddImage(imagePath, 1, 100f, 100f, 200f, 200f);
                mend.Save("output.pdf");
            }

            // Step 5: Record modified PDF size (in bytes)
            byte[] modifiedBytes = File.ReadAllBytes("output.pdf");
            long modifiedSize = modifiedBytes.Length;

            // Step 6: Simple verification – size should increase after adding an image
            if (modifiedSize > originalSize)
            {
                Console.WriteLine($"Test Passed: PDF size increased from {originalSize} to {modifiedSize} bytes.");
            }
            else
            {
                Console.WriteLine($"Test Failed: PDF size did not increase as expected (original: {originalSize}, modified: {modifiedSize}).");
            }
        }
    }
}
