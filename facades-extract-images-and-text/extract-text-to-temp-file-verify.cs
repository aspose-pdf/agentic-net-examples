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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a temporary file for the extracted text
        string tempTxt = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".txt");

        // Use PdfExtractor (Facade) to extract text
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // Perform text extraction (Unicode encoding by default)
            extractor.ExtractText();

            // Get the extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);                     // Save text to stream
                string extractedFromMemory = Encoding.Unicode.GetString(ms.ToArray());

                // Also save the extracted text to the temporary file
                extractor.GetText(tempTxt);                // Save text to file

                // Read the text back from the temporary file
                string extractedFromFile = File.ReadAllText(tempTxt, Encoding.Unicode);

                // Verify that both contents match
                bool isMatch = extractedFromMemory.Equals(extractedFromFile, StringComparison.Ordinal);
                Console.WriteLine(isMatch
                    ? "Verification succeeded: extracted text matches the file content."
                    : "Verification failed: mismatch between extracted text and file content.");

                // Optionally, clean up the temporary file
                try { File.Delete(tempTxt); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}