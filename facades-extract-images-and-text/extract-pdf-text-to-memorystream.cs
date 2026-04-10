using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // <-- added namespace for TextFragment

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a PDF document entirely in memory (no file I/O).
        // ------------------------------------------------------------
        byte[] pdfBytes;
        using (var doc = new Document())
        {
            // Add a page with a simple text fragment so that extraction has content.
            var page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Hello, Aspose PDF!"));

            // Save the document to a temporary MemoryStream and capture the byte array.
            using (var tempStream = new MemoryStream())
            {
                doc.Save(tempStream);
                pdfBytes = tempStream.ToArray();
            }
        }

        // ------------------------------------------------------------
        // 2. Load the PDF bytes into a MemoryStream and extract text.
        // ------------------------------------------------------------
        using (var inputPdf = new MemoryStream(pdfBytes))
        using (var extractor = new PdfExtractor())
        {
            // Bind the in‑memory PDF.
            extractor.BindPdf(inputPdf);

            // Perform the extraction – default is Unicode encoding.
            extractor.ExtractText();

            // Capture the extracted text into another MemoryStream.
            using (var outputText = new MemoryStream())
            {
                extractor.GetText(outputText);
                outputText.Position = 0; // rewind for reading

                // Convert the extracted bytes to a string.
                string extractedText = new StreamReader(outputText, Encoding.Unicode).ReadToEnd();

                Console.WriteLine("Extracted Text:");
                Console.WriteLine(extractedText);
            }
        }
    }
}
