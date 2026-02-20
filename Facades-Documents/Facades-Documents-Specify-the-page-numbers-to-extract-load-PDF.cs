using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Output text file path
        const string outputTextPath = "extracted.txt";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Create a PdfExtractor facade
        PdfExtractor extractor = new PdfExtractor();

        // Bind the PDF document to the extractor
        extractor.BindPdf(pdfPath);

        // Specify the page range to work with (example: pages 2 through 4)
        extractor.StartPage = 2; // first page to extract (1‑based index)
        extractor.EndPage   = 4; // last page to extract

        // -------------------- Extract Text --------------------
        extractor.ExtractText(); // enable text extraction for the defined range

        // Retrieve the extracted text into a memory stream
        using (MemoryStream textStream = new MemoryStream())
        {
            extractor.GetText(textStream);
            string extractedText = Encoding.Unicode.GetString(textStream.ToArray());

            // Save the extracted text to a file
            File.WriteAllText(outputTextPath, extractedText, Encoding.Unicode);
        }

        // -------------------- Extract Images --------------------
        // Enable image extraction for the same page range
        extractor.ExtractImage();

        int imageIndex = 0;
        while (extractor.HasNextImage())
        {
            // Save each extracted image as a separate PNG file
            string imagePath = $"image_{++imageIndex}.png";
            extractor.GetNextImage(imagePath);
        }

        Console.WriteLine("Extraction completed successfully.");
    }
}