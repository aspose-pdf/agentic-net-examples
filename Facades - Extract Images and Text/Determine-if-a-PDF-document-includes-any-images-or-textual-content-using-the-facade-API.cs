using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfExtractor is a facade that implements IDisposable.
        // Use a using block to ensure proper resource cleanup.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor.
            extractor.BindPdf(inputPdf);

            // -------------------------------------------------
            // Check for images
            // -------------------------------------------------
            // ExtractImage must be called before HasNextImage().
            extractor.ExtractImage();
            bool hasImages = extractor.HasNextImage();

            // -------------------------------------------------
            // Check for textual content
            // -------------------------------------------------
            // ExtractText must be called before HasNextPageText().
            extractor.ExtractText();
            bool hasText = extractor.HasNextPageText();

            Console.WriteLine($"PDF contains images: {hasImages}");
            Console.WriteLine($"PDF contains text:   {hasText}");
        }
    }
}