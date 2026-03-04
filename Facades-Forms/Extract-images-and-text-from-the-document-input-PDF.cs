using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputText = "extracted_text.txt";
        const string imagePattern = "image-{0}.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Initialize the facade with the source PDF
            extractor.BindPdf(inputPdf);

            // ----------- Extract Text -----------
            extractor.ExtractText();               // Perform text extraction
            extractor.GetText(outputText);         // Save all extracted text to a file

            // ----------- Extract Images ----------
            extractor.ExtractImage();              // Perform image extraction
            int imgIndex = 1;
            while (extractor.HasNextImage())
            {
                string imgPath = string.Format(imagePattern, imgIndex);
                // Save each image; format is inferred from the file extension
                extractor.GetNextImage(imgPath);
                imgIndex++;
            }
        }

        Console.WriteLine("Text and images have been extracted successfully.");
    }
}