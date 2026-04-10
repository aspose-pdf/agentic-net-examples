using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfTextExtractorFromBytes
{
    // Extracts text from a PDF contained in a byte array.
    // Returns the extracted text as a string.
    public static string ExtractText(byte[] pdfBytes)
    {
        if (pdfBytes == null || pdfBytes.Length == 0)
            throw new ArgumentException("PDF byte array is null or empty.", nameof(pdfBytes));

        // Wrap the byte array in a MemoryStream (no disk I/O).
        using (var pdfStream = new MemoryStream(pdfBytes))
        using (var extractor = new PdfExtractor())
        {
            // Bind the PDF data from the stream.
            extractor.BindPdf(pdfStream);

            // Perform text extraction.
            extractor.ExtractText();

            // Capture the extracted text into another MemoryStream.
            using (var textStream = new MemoryStream())
            {
                extractor.GetText(textStream);

                // Aspose returns UTF‑8 encoded bytes by default.
                return Encoding.UTF8.GetString(textStream.ToArray());
            }
        }
    }

    // Helper that creates a tiny PDF in memory – useful for demo / testing.
    private static byte[] CreateSamplePdf()
    {
        using (var doc = new Document())
        {
            var page = doc.Pages.Add();
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Hello, Aspose PDF!"));
            using (var ms = new MemoryStream())
            {
                doc.Save(ms);
                return ms.ToArray();
            }
        }
    }

    // Example usage.
    static void Main()
    {
        // Generate a PDF in memory – no file system access required.
        byte[] pdfData = CreateSamplePdf();

        string text = ExtractText(pdfData);
        Console.WriteLine("Extracted Text:");
        Console.WriteLine(text);
    }
}