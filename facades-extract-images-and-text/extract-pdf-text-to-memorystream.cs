using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added namespace for TextFragment

class Program
{
    static void Main()
    {
        // Create a sample PDF entirely in memory so no external file is required.
        using (MemoryStream inputStream = new MemoryStream())
        {
            CreateSamplePdf(inputStream);
            // Reset the position to the beginning before binding.
            inputStream.Position = 0;

            using (MemoryStream outputStream = new MemoryStream())
            {
                // Extract text from the PDF using PdfExtractor.
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(inputStream);
                    extractor.ExtractText();
                    extractor.GetText(outputStream);
                }

                // Read the extracted text from the output memory stream.
                outputStream.Position = 0;
                // PdfExtractor writes UTF‑8 text, so use UTF8 encoding when converting to string.
                string extractedText = Encoding.UTF8.GetString(outputStream.ToArray());
                Console.WriteLine("Extracted text:");
                Console.WriteLine(extractedText);
            }
        }
    }

    // Helper that builds a minimal PDF with some text and writes it to the provided stream.
    private static void CreateSamplePdf(Stream stream)
    {
        Document doc = new Document();
        Page page = doc.Pages.Add();
        page.Paragraphs.Add(new TextFragment("Hello Aspose PDF!"));
        doc.Save(stream, SaveFormat.Pdf);
    }
}
