using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use PdfExtractor (Facade) inside a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Extract images from the document (no need to specify a folder)
            extractor.ExtractImage();

            int imageCount = 0;

            // Iterate through all extracted images.
            // GetNextImage advances the internal pointer, so we must call it to move forward.
            while (extractor.HasNextImage())
            {
                // Retrieve the next image into a dummy stream (we don't need to save it)
                using (MemoryStream dummy = new MemoryStream())
                {
                    extractor.GetNextImage(dummy);
                }
                imageCount++;
            }

            // Determine if the PDF is text‑only
            bool isTextOnly = imageCount == 0;

            Console.WriteLine(isTextOnly
                ? "The PDF contains only text (no images were found)."
                : $"The PDF contains images ({imageCount} image(s) found).");
        }
    }
}