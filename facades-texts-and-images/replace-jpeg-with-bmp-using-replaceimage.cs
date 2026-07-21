using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Create a source PDF that contains a JPEG image on the first page.
        //    This makes the example self‑contained – no external "input.pdf" is
        //    required in the sandbox.
        // ---------------------------------------------------------------------
        const string inputPdfPath = "input.pdf";   // temporary file for demo only
        const string outputPdfPath = "output.pdf";

        // Create a small JPEG image in memory (simulating the original low‑res image).
        using (var lowResBmp = new Bitmap(50, 50))
        using (var lowResGraphics = Graphics.FromImage(lowResBmp))
        using (var lowResJpegStream = new MemoryStream())
        {
            lowResGraphics.Clear(System.Drawing.Color.LightBlue);
            lowResBmp.Save(lowResJpegStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            lowResJpegStream.Position = 0;

            // Build the PDF and embed the JPEG image.
            using (var pdfDoc = new Document())
            {
                Page page = pdfDoc.Pages.Add();
                // Aspose.Pdf.Image does not have a constructor that accepts a Stream.
                // Use the parameterless constructor and assign the ImageStream property.
                var jpegImage = new Aspose.Pdf.Image();
                jpegImage.ImageStream = lowResJpegStream;
                // Position the image (optional).
                jpegImage.FixWidth = 100;
                jpegImage.FixHeight = 100;
                page.Paragraphs.Add(jpegImage);
                pdfDoc.Save(inputPdfPath);
            }
        }

        // ---------------------------------------------------------------------
        // 2. Prepare a higher‑resolution BMP image as a stream.
        // ---------------------------------------------------------------------
        MemoryStream highResBmpStream;
        using (var highResBmp = new Bitmap(200, 200))
        using (var gfx = Graphics.FromImage(highResBmp))
        {
            gfx.Clear(System.Drawing.Color.OrangeRed);
            // Draw something to make the image recognisable.
            gfx.DrawEllipse(System.Drawing.Pens.White, 20, 20, 160, 160);
            highResBmpStream = new MemoryStream();
            highResBmp.Save(highResBmpStream, System.Drawing.Imaging.ImageFormat.Bmp);
            highResBmpStream.Position = 0; // reset for reading
        }

        // ---------------------------------------------------------------------
        // 3. Load the PDF, bind it to PdfContentEditor and replace the image.
        // ---------------------------------------------------------------------
        // PdfContentEditor.ReplaceImage overload that accepts a Stream is not
        // available in the version used for this sandbox, therefore we write the
        // BMP to a temporary file and pass the file path.
        string tempBmpPath = Path.GetTempFileName();
        File.WriteAllBytes(tempBmpPath, highResBmpStream.ToArray());

        using (Document pdfDoc = new Document(inputPdfPath))
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(pdfDoc);
            // Replace the first image (index = 1) on page 1 with the BMP file.
            editor.ReplaceImage(pageNumber: 1, index: 1, tempBmpPath);
            pdfDoc.Save(outputPdfPath);
        }

        // Clean up temporary files.
        if (File.Exists(inputPdfPath))
            File.Delete(inputPdfPath);
        if (File.Exists(tempBmpPath))
            File.Delete(tempBmpPath);

        Console.WriteLine($"Image replaced and saved to '{outputPdfPath}'.");
    }
}
