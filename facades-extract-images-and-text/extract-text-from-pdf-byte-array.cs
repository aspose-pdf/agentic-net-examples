using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Simple console‑based entry point. The first argument is expected to be the path to a PDF file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: PdfExtractionApp <pdf-file-path>");
            return;
        }

        string pdfPath = args[0];
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Read the PDF into a byte array – this mimics receiving a byte[] payload in a REST call.
        byte[] pdfBytes = File.ReadAllBytes(pdfPath);
        string extractedText = ExtractTextFromPdf(pdfBytes);

        // Serialize the result as JSON (equivalent to returning Ok(new { text = ... }) in ASP.NET).
        var result = new { text = extractedText };
        string json = JsonSerializer.Serialize(result);
        Console.WriteLine(json);
    }

    private static string ExtractTextFromPdf(byte[] pdfBytes)
    {
        // The core Aspose.Pdf.Facades extraction logic remains unchanged.
        using (var inputStream = new MemoryStream(pdfBytes))
        using (var extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputStream);
            extractor.ExtractText(); // default pure‑text extraction mode

            using (var outputStream = new MemoryStream())
            {
                extractor.GetText(outputStream);
                // Aspose returns Unicode (UTF‑16LE) by default; convert to a .NET string.
                return Encoding.Unicode.GetString(outputStream.ToArray());
            }
        }
    }
}
