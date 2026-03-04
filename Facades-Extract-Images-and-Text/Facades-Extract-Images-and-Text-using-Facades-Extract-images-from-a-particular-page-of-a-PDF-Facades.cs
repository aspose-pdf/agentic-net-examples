using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const int targetPage = 2;                     // page to extract (1‑based)
        const string outputText = "page2.txt";        // extracted text file
        const string imageOutputDir = "Page2Images";  // folder for extracted images

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the image output directory exists
        Directory.CreateDirectory(imageOutputDir);

        // PdfExtractor implements IDisposable – use a using block for deterministic cleanup
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF file into the facade
            extractor.BindPdf(inputPdf);

            // Restrict extraction to the desired page
            extractor.StartPage = targetPage;
            extractor.EndPage   = targetPage;

            // ---------- Extract text ----------
            extractor.ExtractText();                 // perform text extraction for the set page range
            extractor.GetNextPageText(outputText);   // save the extracted text to a file

            // ---------- Extract images ----------
            extractor.ExtractImage();                // prepare image extraction for the set page range

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build a unique file name for each image
                string imagePath = Path.Combine(imageOutputDir, $"image_{imageIndex}.png");
                // Save the next image; the file extension determines the image format
                extractor.GetNextImage(imagePath);
                imageIndex++;
            }
        }

        Console.WriteLine("Extraction of text and images completed.");
    }
}