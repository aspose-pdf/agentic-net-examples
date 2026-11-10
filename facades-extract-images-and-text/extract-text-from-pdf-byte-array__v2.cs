using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main(string[] args)
    {
        // The console version expects the path to a PDF file as the first argument.
        // It reads the file, extracts the text with Aspose.Pdf, and writes a JSON
        // object { "text": "..." } to the standard output.
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

        byte[] pdfBytes = await File.ReadAllBytesAsync(pdfPath);
        string extractedText = await ExtractTextAsync(pdfBytes);

        var result = new { text = extractedText };
        string json = JsonSerializer.Serialize(result);
        Console.WriteLine(json);
    }

    private static async Task<string> ExtractTextAsync(byte[] pdfBytes)
    {
        // Use Aspose.Pdf.Facades.PdfExtractor to pull the text out of the PDF.
        using var extractor = new PdfExtractor();
        using var pdfStream = new MemoryStream(pdfBytes);
        extractor.BindPdf(pdfStream);
        extractor.ExtractText();

        await using var textStream = new MemoryStream();
        extractor.GetText(textStream);
        textStream.Position = 0;
        using var reader = new StreamReader(textStream);
        return await reader.ReadToEndAsync();
    }
}
