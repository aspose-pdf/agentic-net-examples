using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Simple console‑based “endpoint”.
        // Pass the path to a PDF file as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: AsposePdfApi <pdf-file-path>");
            return;
        }

        string pdfPath = args[0];
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        byte[] pdfBytes = File.ReadAllBytes(pdfPath);
        string extractedText = ExtractTextFromPdf(pdfBytes);

        var result = new { text = extractedText };
        string json = JsonSerializer.Serialize(result);
        Console.WriteLine(json);
    }

    private static string ExtractTextFromPdf(byte[] pdfBytes)
    {
        // Use Aspose.Pdf.Facades.PdfExtractor to pull text from the PDF.
        using var extractor = new PdfExtractor();
        using var pdfStream = new MemoryStream(pdfBytes);
        extractor.BindPdf(pdfStream);
        extractor.ExtractText();

        using var textStream = new MemoryStream();
        extractor.GetText(textStream);
        // Aspose returns Unicode (UTF‑16LE) bytes.
        return Encoding.Unicode.GetString(textStream.ToArray());
    }
}
