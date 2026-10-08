using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added to resolve TextFragment

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a minimal PDF entirely in memory (no file I/O).
        // ------------------------------------------------------------
        byte[] pdfBytes;
        using (var doc = new Document())
        {
            // Add a page and some sample text so that extraction has content.
            var page = doc.Pages.Add();
            var text = new TextFragment("Hello, Aspose PDF!");
            page.Paragraphs.Add(text);

            // Save the document to a MemoryStream and capture the byte array.
            using (var ms = new MemoryStream())
            {
                doc.Save(ms);
                pdfBytes = ms.ToArray();
            }
        }

        // ------------------------------------------------------------
        // 2. Load the PDF bytes into a MemoryStream for the extractor.
        // ------------------------------------------------------------
        using var inputStream = new MemoryStream(pdfBytes);
        inputStream.Position = 0; // ensure the stream starts at the beginning

        // ------------------------------------------------------------
        // 3. Extract text using PdfExtractor (still fully in‑memory).
        // ------------------------------------------------------------
        using var extractor = new PdfExtractor();
        extractor.BindPdf(inputStream);
        extractor.ExtractText();

        // ------------------------------------------------------------
        // 4. Write the extracted text to another MemoryStream.
        // ------------------------------------------------------------
        using var outputStream = new MemoryStream();
        extractor.GetText(outputStream);
        outputStream.Position = 0; // reset for reading

        // ------------------------------------------------------------
        // 5. Read the text back as a string (example of further processing).
        // ------------------------------------------------------------
        using var reader = new StreamReader(outputStream);
        string extractedText = reader.ReadToEnd();
        Console.WriteLine(extractedText);
    }
}