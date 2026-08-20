using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // For TextFragment

class Program
{
    // Creates a simple PDF in memory and returns its bytes.
    // No external files are read or written to disk.
    static byte[] ConvertPdfToByteArray()
    {
        // 1. Build a sample PDF document entirely in memory.
        using (var doc = new Document())
        {
            // Add a page and some content so the PDF is not empty.
            var page = doc.Pages.Add();
            var paragraph = new TextFragment("This is a sample PDF created in‑memory.");
            page.Paragraphs.Add(paragraph);

            // 2. Save the document to a MemoryStream.
            using (var memoryStream = new MemoryStream())
            {
                doc.Save(memoryStream);
                // Ensure the stream position is at the beginning before reading.
                memoryStream.Position = 0;
                // 3. Return the raw bytes. No PdfViewer needed.
                return memoryStream.ToArray();
            }
        }
    }

    static void Main()
    {
        // Convert the in‑memory PDF to a byte array.
        byte[] pdfBytes = ConvertPdfToByteArray();

        // Example usage: output the size of the byte array.
        Console.WriteLine($"PDF byte array length: {pdfBytes.Length}");
        // The byte array can now be sent over a web API without ever touching the file system.
    }
}
