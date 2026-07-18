using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // <-- required for TextFragment

class PdfTextExtractorFromBytes
{
    static void Main()
    {
        // Create a simple PDF in memory and obtain its bytes
        byte[] pdfData = CreateSamplePdf();

        // Extract text from the PDF byte array without writing to disk
        string extractedText = ExtractTextFromPdfBytes(pdfData);

        Console.WriteLine("Extracted Text:");
        Console.WriteLine(extractedText);
    }

    // Generates a one‑page PDF containing a single line of text and returns the bytes
    private static byte[] CreateSamplePdf()
    {
        using (var doc = new Document())
        {
            // Add a page
            var page = doc.Pages.Add();

            // Add a text fragment
            var tf = new TextFragment("Hello, Aspose.PDF!");
            page.Paragraphs.Add(tf);

            // Save to a memory stream
            using (var ms = new MemoryStream())
            {
                doc.Save(ms);
                return ms.ToArray();
            }
        }
    }

    private static string ExtractTextFromPdfBytes(byte[] pdfBytes)
    {
        // Input stream containing the PDF data
        using (var inputStream = new MemoryStream(pdfBytes))
        // Facade for extracting text
        using (var extractor = new PdfExtractor())
        {
            // Bind the PDF from the stream
            extractor.BindPdf(inputStream);

            // Perform text extraction (Unicode encoding by default)
            extractor.ExtractText();

            // Capture the extracted text into an output stream
            using (var outputStream = new MemoryStream())
            {
                extractor.GetText(outputStream);

                // Convert the output bytes (Unicode) to a string
                return Encoding.Unicode.GetString(outputStream.ToArray());
            }
        }
    }
}