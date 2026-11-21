using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    // Path to the known sample PDF file. Adjust as needed for your test environment.
    private const string SamplePdfPath = "sample.pdf";

    // The exact text expected to be extracted from the sample PDF.
    // Ensure this matches the content of sample.pdf.
    private const string ExpectedText = "Hello World";

    static void Main()
    {
        // Verify that the sample PDF exists before running the check.
        if (!File.Exists(SamplePdfPath))
        {
            Console.WriteLine($"FAIL: Sample PDF not found at path: {SamplePdfPath}");
            return;
        }

        // PdfExtractor implements IDisposable, so wrap it in a using block for deterministic disposal.
        using (var extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor.
            extractor.BindPdf(SamplePdfPath);

            // Enable text extraction. Use an explicit encoding because GetText requires a stream.
            extractor.ExtractText(Encoding.UTF8);

            // Retrieve the extracted text via a MemoryStream (GetText has no parameter‑less overload).
            string extractedText;
            using (var ms = new MemoryStream())
            {
                extractor.GetText(ms);
                extractedText = Encoding.UTF8.GetString(ms.ToArray()).Trim();
            }

            // Simple pass/fail output – no test framework required.
            if (extractedText == ExpectedText)
            {
                Console.WriteLine("PASS: Extracted text matches expected content.");
            }
            else
            {
                Console.WriteLine($"FAIL: Extracted text does not match. Expected \"{ExpectedText}\", got \"{extractedText}\"");
            }
        }
    }
}