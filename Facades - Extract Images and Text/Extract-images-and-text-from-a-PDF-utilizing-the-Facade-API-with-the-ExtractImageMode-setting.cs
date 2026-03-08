using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "sample.pdf";          // PDF to process
        const string outputText = "extracted_text.txt"; // Text output file
        const string imagePrefix = "image_";            // Prefix for extracted images

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfExtractor implements IDisposable, so wrap it in a using block.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor.
            extractor.BindPdf(inputPdf);

            // OPTIONAL: limit extraction to a page range.
            // extractor.StartPage = 1;
            // extractor.EndPage   = extractor.Document.Pages.Count;

            // The ExtractImageMode property is not available in the current Aspose.Pdf.Facades version.
            // Image extraction is performed by calling ExtractImage() which extracts all images by default.
            // extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed; // <-- removed for compatibility

            // Extract images.
            extractor.ExtractImage();
            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each image to a separate file (default format is JPEG).
                string imagePath = $"{imagePrefix}{imageIndex}.jpg";
                extractor.GetNextImage(imagePath);
                Console.WriteLine($"Image saved: {imagePath}");
                imageIndex++;
            }

            // Extract text.
            extractor.ExtractText(); // default pure text mode (0)
            // Save all extracted text to a single file.
            extractor.GetText(outputText);
            Console.WriteLine($"Text saved: {outputText}");
        }
    }
}
