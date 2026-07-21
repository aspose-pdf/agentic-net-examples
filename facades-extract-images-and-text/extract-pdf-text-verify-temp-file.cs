using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Extract text to memory and to a temporary file, then verify they match
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(inputPdf);

            // Perform text extraction (Unicode encoding is default)
            extractor.ExtractText();

            // Get extracted text into a MemoryStream
            string extractedText;
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);
                extractedText = Encoding.Unicode.GetString(ms.ToArray());
            }

            // Write the same extracted text to a temporary file
            string tempFile = Path.GetTempFileName();
            extractor.GetText(tempFile);

            // Read the text back from the temporary file
            string fileText = File.ReadAllText(tempFile, Encoding.Unicode);

            // Verify that both strings are identical
            bool match = extractedText == fileText;
            Console.WriteLine(match
                ? "Extracted text matches file content."
                : "Mismatch between extracted text and file content.");

            // Clean up the temporary file
            try { File.Delete(tempFile); } catch { }
        }
    }
}