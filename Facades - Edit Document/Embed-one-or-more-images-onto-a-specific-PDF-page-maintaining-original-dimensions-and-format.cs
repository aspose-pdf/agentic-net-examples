using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.jpg";
        const int targetPage = 2; // 1‑based page index

        // Validate input files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Obtain original image dimensions using Aspose.Pdf.Image
        var img = new Image { File = imagePath };
        double imgWidth = img.BitmapSize.Width;
        double imgHeight = img.BitmapSize.Height;

        // Load the PDF document
        using (var pdfDoc = new Document(inputPdf))
        {
            // Bind the facade to the loaded document
            using (var mend = new PdfFileMend(pdfDoc))
            {
                // Add the image to the specified page.
                // PdfFileMend.AddImage expects a Stream for the image and float values for the rectangle.
                using (var imageStream = File.OpenRead(imagePath))
                {
                    bool added = mend.AddImage(
                        imageStream,               // Stream containing the image
                        targetPage,                // 1‑based page number
                        0f,                        // lower‑left X (float)
                        0f,                        // lower‑left Y (float)
                        (float)imgWidth,           // upper‑right X (float) – original width
                        (float)imgHeight);         // upper‑right Y (float) – original height

                    if (!added)
                    {
                        Console.Error.WriteLine("Failed to embed the image.");
                        return;
                    }
                }

                // Save the modified PDF to the destination file
                mend.Save(outputPdf);
            }
        }

        Console.WriteLine($"Image successfully embedded on page {targetPage} of '{outputPdf}'.");
    }
}
