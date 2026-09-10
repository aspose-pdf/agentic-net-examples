using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades; // PdfExtractor resides here

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }

        // Create a temporary file for the extracted text
        string tempTextFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".txt");

        // Use PdfExtractor (Facade) to extract text
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(inputPdf);

            // Perform the extraction (Unicode encoding is default)
            extractor.ExtractText();

            // 1) Extract text to a memory stream to keep the original string in memory
            string originalText;
            using (MemoryStream memStream = new MemoryStream())
            {
                extractor.GetText(memStream);
                originalText = Encoding.Unicode.GetString(memStream.ToArray());
            }

            // 2) Save the extracted text to the temporary file
            extractor.GetText(tempTextFile);

            // Read the text back from the temporary file
            string fileText = File.ReadAllText(tempTextFile, Encoding.Unicode);

            // Verify that both strings are identical
            bool isMatch = string.Equals(originalText, fileText, StringComparison.Ordinal);
            Console.WriteLine(isMatch
                ? "Verification succeeded: extracted text matches the file content."
                : "Verification failed: mismatch between extracted text and file content.");
        }

        // Clean up the temporary file
        try
        {
            if (File.Exists(tempTextFile))
                File.Delete(tempTextFile);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary file: {ex.Message}");
        }
    }
}