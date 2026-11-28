using System;
using System.IO;
using System.Drawing;                     // Image, Bitmap (used only for size extraction)
using Aspose.Pdf.Facades;                // PdfExtractor, PdfFileMend

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Extract the first image from the source PDF using PdfExtractor
        // -----------------------------------------------------------------
        Image extractedImage = null;
        byte[] imageBytes = null;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // Extract images from the document
            extractor.ExtractImage();

            // Retrieve the first extracted image (if any) into a MemoryStream
            if (extractor.HasNextImage())
            {
                using (var imgStream = new MemoryStream())
                {
                    // GetNextImage writes the image bytes into the provided stream
                    extractor.GetNextImage(imgStream);
                    imgStream.Position = 0;
                    // Keep the raw bytes for later use with PdfFileMend
                    imageBytes = imgStream.ToArray();
                    // Create a System.Drawing.Image only to obtain width/height
                    extractedImage = Image.FromStream(new MemoryStream(imageBytes));
                }
            }
        }

        if (extractedImage == null || imageBytes == null)
        {
            Console.Error.WriteLine("No images were extracted from the PDF.");
            return;
        }

        // -----------------------------------------------------------------
        // 2. Open the PDF for modification with PdfFileMend and add the image
        // -----------------------------------------------------------------
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Bind the source PDF (creates an editable copy in memory)
            mend.BindPdf(inputPdf);

            // Target page number (PdfFileMend expects a single int, not an array)
            int targetPage = 35;

            // Define lower‑left (llx, lly) and upper‑right (urx, ury) coordinates.
            // Here we place the image at (150,150) and keep its original size.
            float llx = 150f;
            float lly = 150f;
            float urx = llx + extractedImage.Width;   // right edge
            float ury = lly + extractedImage.Height;  // top edge

            // Add the image to the specified page using a stream overload
            using (var imgStreamForMend = new MemoryStream(imageBytes))
            {
                mend.AddImage(imgStreamForMend, targetPage, llx, lly, urx, ury);
            }

            // Save the modified PDF
            mend.Save(outputPdf);
        }

        // Dispose the extracted image explicitly
        extractedImage.Dispose();

        Console.WriteLine($"Image added to page 35 and saved as '{outputPdf}'.");
    }
}
