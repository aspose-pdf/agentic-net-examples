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
            Console.Error.WriteLine("Usage: AsposePdfApi <pdf-file-path>");
            return;
        }

        string pdfPath = args[0];
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Read the PDF into a memory stream (Aspose.Pdf.Facades works with streams).
        using var pdfStream = new MemoryStream(File.ReadAllBytes(pdfPath));

        // Extract all text from the document.
        using var extractor = new PdfExtractor();
        extractor.BindPdf(pdfStream);
        extractor.ExtractText();

        // Retrieve the extracted text.
        using var textStream = new MemoryStream();
        extractor.GetText(textStream);
        string extractedText = Encoding.UTF8.GetString(textStream.ToArray());

        // Emit the result as JSON to stdout.
        var result = new { text = extractedText };
        string json = JsonSerializer.Serialize(result);
        Console.WriteLine(json);
    }
}
