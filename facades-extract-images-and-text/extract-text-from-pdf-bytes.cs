using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // <-- added namespace for TextFragment

class PdfTextExtractor
{
    // Extracts all text from a PDF provided as a byte array.
    // Returns the extracted text as a string.
    public static string ExtractTextFromPdfBytes(byte[] pdfBytes)
    {
        if (pdfBytes == null || pdfBytes.Length == 0)
            throw new ArgumentException("PDF byte array is null or empty.", nameof(pdfBytes));

        // Wrap the byte array in a memory stream (no disk I/O).
        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF data from the stream.
            extractor.BindPdf(pdfStream);

            // Perform text extraction using the default Unicode encoding.
            extractor.ExtractText();

            // Capture the extracted text into another memory stream.
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);

                // Convert the raw bytes (Unicode) to a .NET string.
                return Encoding.Unicode.GetString(textStream.ToArray());
            }
        }
    }

    // Example usage – creates a PDF in memory, extracts its text, and prints it.
    static void Main()
    {
        // Create a simple PDF document entirely in memory.
        byte[] pdfBytes;
        using (MemoryStream ms = new MemoryStream())
        {
            Document doc = new Document();
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Hello Aspose PDF!"));
            doc.Save(ms);
            pdfBytes = ms.ToArray();
        }

        // Extract text from the in‑memory PDF bytes.
        string text = ExtractTextFromPdfBytes(pdfBytes);
        Console.WriteLine("Extracted Text:");
        Console.WriteLine(text);
    }
}
