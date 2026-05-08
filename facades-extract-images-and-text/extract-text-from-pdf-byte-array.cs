using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect a single argument: path to the PDF file to process
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: dotnet run <pdf-file-path>");
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

        // Return the extracted text as a JSON object on stdout
        var result = new { text = extractedText };
        string json = JsonSerializer.Serialize(result);
        Console.WriteLine(json);
    }

    private static string ExtractTextFromPdf(byte[] pdfBytes)
    {
        if (pdfBytes == null || pdfBytes.Length == 0)
            return string.Empty;

        using var pdfStream = new MemoryStream(pdfBytes);
        using var extractor = new PdfExtractor();
        extractor.BindPdf(pdfStream);   // bind the PDF from the stream
        extractor.ExtractText();        // perform text extraction (pure mode)

        using var textStream = new MemoryStream();
        extractor.GetText(textStream);
        // Aspose returns UTF‑16LE (Unicode) bytes
        return Encoding.Unicode.GetString(textStream.ToArray());
    }
}
