using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Simple console‑based "endpoint": the first argument is the path to the PDF file.
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

        byte[] pdfData = File.ReadAllBytes(pdfPath);
        string extractedText = ExtractText(pdfData);

        // Return the extracted text as a JSON string on stdout.
        var result = new { text = extractedText };
        string json = JsonSerializer.Serialize(result);
        Console.WriteLine(json);
    }

    private static string ExtractText(byte[] pdfBytes)
    {
        // Use Aspose.Pdf.Facades.PdfExtractor to pull text from the PDF.
        using var pdfStream = new MemoryStream(pdfBytes);
        using var extractor = new PdfExtractor();
        extractor.BindPdf(pdfStream);
        extractor.ExtractText();

        using var textStream = new MemoryStream();
        extractor.GetText(textStream);
        // Aspose returns Unicode text by default; decode accordingly.
        return Encoding.Unicode.GetString(textStream.ToArray());
    }
}
