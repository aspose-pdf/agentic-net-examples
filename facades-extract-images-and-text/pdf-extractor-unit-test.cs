using System;
using System.IO;

public class PdfExtractorUnitTest
{
    // Entry point required for compilation
    public static void Main(string[] args)
    {
        // Path to the known sample PDF file
        const string samplePdfPath = "sample.pdf";

        // Expected text content of the sample PDF (adjust to match the actual file)
        const string expectedText = "Hello World";

        // Verify the sample file exists
        if (!File.Exists(samplePdfPath))
        {
            Console.Error.WriteLine($"Sample PDF not found: {samplePdfPath}");
            return;
        }

        // Extract text using PdfExtractor
        string extractedText = ExtractTextFromPdf(samplePdfPath);

        // Simple assertion – compare extracted text with the expected value
        if (extractedText.Trim() == expectedText)
        {
            Console.WriteLine("PdfExtractor test passed.");
        }
        else
        {
            Console.WriteLine("PdfExtractor test failed.");
            Console.WriteLine($"Expected: \"{expectedText}\"");
            Console.WriteLine($"Extracted: \"{extractedText}\"");
        }
    }

    // Helper method that encapsulates the extraction logic
    private static string ExtractTextFromPdf(string pdfPath)
    {
        // PdfExtractor resides in Aspose.Pdf.Facades
        using (Aspose.Pdf.Facades.PdfExtractor extractor = new Aspose.Pdf.Facades.PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(pdfPath);

            // Instruct the extractor to extract text
            extractor.ExtractText();

            // GetText requires a destination stream – use a MemoryStream
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream); // writes extracted text into the stream
                textStream.Position = 0;       // reset position for reading

                // Read the stream content as a string
                using (StreamReader reader = new StreamReader(textStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}