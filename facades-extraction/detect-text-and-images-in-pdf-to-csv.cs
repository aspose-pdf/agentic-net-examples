using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "result.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        bool containsText = false;
        bool containsImages = false;

        // Use PdfExtractor to analyze the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);

            // ----- Text detection -----
            extractor.ExtractText();
            string extractedText;
            using (MemoryStream textStream = new MemoryStream())
            {
                // GetText writes the extracted text into the provided stream
                extractor.GetText(textStream);
                extractedText = Encoding.UTF8.GetString(textStream.ToArray());
            }
            containsText = !string.IsNullOrWhiteSpace(extractedText);

            // ----- Image detection -----
            extractor.ExtractImage();
            int imageCount = 0;
            while (extractor.HasNextImage())
            {
                // Retrieve each image into a memory stream just to advance the iterator
                using (MemoryStream imgStream = new MemoryStream())
                {
                    extractor.GetNextImage(imgStream);
                }
                imageCount++;
            }
            containsImages = imageCount > 0;
        }

        // Write the detection results to a CSV file
        try
        {
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                writer.WriteLine("ContainsText,ContainsImages");
                writer.WriteLine($"{containsText},{containsImages}");
            }
            Console.WriteLine($"Detection results saved to '{outputCsvPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing CSV: {ex.Message}");
        }
    }
}
