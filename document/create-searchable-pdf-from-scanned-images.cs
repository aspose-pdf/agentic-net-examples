using System;
using System.IO;
using Aspose.Pdf;
// Optional: include if you need the OCR delegate type explicitly
// using Aspose.Pdf.Ocr;

class Program
{
    static void Main()
    {
        const string inputPath = "scanned.pdf";
        const string outputPath = "searchable.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the scanned PDF (image‑only pages)
        using (Document doc = new Document(inputPath))
        {
            // Perform OCR conversion: adds invisible text over the images.
            // The lambda receives the image of the page and its number and must return HOCR XML.
            // Replace the body of this lambda with actual OCR logic if needed.
            doc.Convert(
                (image, pageNumber) => string.Empty, // placeholder OCR implementation
                flattenImages: false);

            // Save the resulting searchable PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Searchable PDF saved to '{outputPath}'.");
    }
}
