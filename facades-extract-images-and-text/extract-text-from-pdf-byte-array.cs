using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Obtain the PDF content as a byte array. Replace the implementation of GetPdfBytes
        // with the actual source of your PDF data (e.g., a database, a web service, an embedded resource, etc.).
        byte[] pdfBytes = GetPdfBytes();

        if (pdfBytes == null || pdfBytes.Length == 0)
        {
            Console.Error.WriteLine("Error: PDF byte array is empty. Provide a valid PDF document.");
            return;
        }

        // Use a MemoryStream to avoid any file system I/O when extracting text.
        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF from the memory stream.
            extractor.BindPdf(pdfStream);

            // Configure the extractor to retrieve text.
            extractor.ExtractText();

            // Write the extracted text to a stream, then read it as a string.
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream); // Write text to the stream.
                textStream.Position = 0;        // Reset position for reading.
                using (StreamReader reader = new StreamReader(textStream))
                {
                    string extractedText = reader.ReadToEnd();
                    // Process the extracted text as needed.
                    Console.WriteLine(extractedText);
                }
            }
        }
    }

    // Example implementation – replace with your real source.
    // This version reads a PDF file from disk only to obtain the byte array.
    // The extraction itself still works entirely in memory.
    static byte[] GetPdfBytes()
    {
        const string samplePath = "sample.pdf"; // Path to a sample PDF for demonstration.
        if (!File.Exists(samplePath))
        {
            Console.Error.WriteLine($"Sample PDF not found at '{samplePath}'.");
            return new byte[0];
        }
        return File.ReadAllBytes(samplePath);
    }
}
