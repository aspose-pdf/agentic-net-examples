using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Temporary text file to store extracted content
        string tempTxt = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".txt");

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Extract text using PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(inputPdf);

            // Perform text extraction (Unicode encoding by default)
            extractor.ExtractText();

            // Capture extracted text into a memory stream
            string extractedText;
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);                     // Write text to stream
                extractedText = Encoding.Unicode.GetString(ms.ToArray());
            }

            // Save the same extracted text to a temporary file using the same encoding
            File.WriteAllText(tempTxt, extractedText, Encoding.Unicode);

            // Read back the text from the temporary file using the same encoding
            string fileContent = File.ReadAllText(tempTxt, Encoding.Unicode);

            // Verify that the in‑memory extraction matches the file content
            bool isMatch = string.Equals(extractedText, fileContent, StringComparison.Ordinal);
            Console.WriteLine(isMatch
                ? "Verification succeeded: extracted text matches file content."
                : "Verification failed: mismatch between extracted text and file content.");
        }

        // Clean up the temporary file
        try { File.Delete(tempTxt); } catch { /* ignore cleanup errors */ }
    }
}
