using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // <-- added for TextFragment

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdfPath = "input.pdf";

        // Ensure the input PDF exists – create a simple one if missing
        if (!File.Exists(inputPdfPath))
        {
            CreateSamplePdf(inputPdfPath);
        }

        // Create a temporary file for the extracted text
        string tempTextPath = Path.ChangeExtension(Path.GetTempFileName(), ".txt");

        // Use PdfExtractor (Facade) to extract text
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(inputPdfPath);

            // Extract text using Unicode encoding (default)
            extractor.ExtractText();

            // Capture the extracted text into a memory stream
            string extractedText;
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);
                // Aspose writes UTF‑8 by default, so decode with UTF8
                extractedText = Encoding.UTF8.GetString(ms.ToArray());
            }

            // Save the extracted text to the temporary file (UTF‑8)
            extractor.GetText(tempTextPath);

            // Read the text back from the temporary file using the same encoding
            string readBackText = File.ReadAllText(tempTextPath, Encoding.UTF8);

            // Verify that the saved text matches the original extraction
            bool isMatch = string.Equals(extractedText, readBackText, StringComparison.Ordinal);
            Console.WriteLine(isMatch
                ? "Verification succeeded: extracted text matches the saved file."
                : "Verification failed: extracted text does NOT match the saved file.");
        }

        // Clean up the temporary file (optional)
        try { File.Delete(tempTextPath); } catch { /* ignore cleanup errors */ }
    }

    private static void CreateSamplePdf(string path)
    {
        // Create a minimal PDF with a single page containing some text
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // Add a simple text paragraph
            page.Paragraphs.Add(new TextFragment("Hello Aspose PDF! This is sample text for extraction."));
            doc.Save(path);
        }
    }
}
