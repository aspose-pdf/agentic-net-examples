using System;
using System.IO;
using System.Drawing;                 // For loading TIFF frames (Windows GDI+)
using System.Drawing.Imaging;        // For FrameDimension
using Aspose.Pdf;                    // Core Aspose.Pdf types

// Alias ambiguous types to avoid CS0104 errors
using SysImage = System.Drawing.Image;
using PdfImage = Aspose.Pdf.Image;

class TiffToPdfConverter
{
    static void Main()
    {
        const string tiffPath = "input.tif";
        const string pdfPath  = "output.pdf";

        if (!File.Exists(tiffPath))
        {
            Console.Error.WriteLine($"TIFF file not found: {tiffPath}");
            return;
        }

        // Load the multi‑frame TIFF using System.Drawing.Image (aliased as SysImage)
        using (SysImage tiffImage = SysImage.FromFile(tiffPath))
        {
            // Determine how many frames/pages the TIFF contains
            int frameCount = tiffImage.GetFrameCount(FrameDimension.Page);

            // Create a new PDF document
            using (Document pdfDoc = new Document())
            {
                // Iterate over each frame and add it as a separate PDF page
                for (int i = 0; i < frameCount; i++)
                {
                    // Select the current frame
                    tiffImage.SelectActiveFrame(FrameDimension.Page, i);

                    // Save the frame to a memory stream in a format supported by Aspose.Pdf (PNG works well)
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        tiffImage.Save(imgStream, ImageFormat.Png);
                        imgStream.Position = 0; // Reset stream position for reading

                        // Add a new page to the PDF
                        Page page = pdfDoc.Pages.Add();

                        // Create an Aspose.Pdf.Image (aliased as PdfImage) and assign the stream
                        PdfImage pdfImage = new PdfImage
                        {
                            ImageStream = imgStream
                        };

                        // Add the image to the page's paragraph collection
                        page.Paragraphs.Add(pdfImage);
                    }
                }

                // Save the assembled PDF (no SaveOptions needed – default is PDF)
                pdfDoc.Save(pdfPath);
            }
        }

        Console.WriteLine($"TIFF converted to PDF successfully: {pdfPath}");
    }
}
