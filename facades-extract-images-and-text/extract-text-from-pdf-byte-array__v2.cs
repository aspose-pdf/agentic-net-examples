using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect a single argument: the path to the PDF file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: <exe> <pdfFilePath>");
            return;
        }

        string pdfPath = args[0];
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Read the PDF into a byte array.
        byte[] pdfBytes = File.ReadAllBytes(pdfPath);

        // Extract text from the PDF.
        string extractedText = ExtractTextFromPdf(pdfBytes);

        // Serialize the result as JSON and write to stdout.
        string json = JsonSerializer.Serialize(new { text = extractedText });
        Console.WriteLine(json);
    }

    private static string ExtractTextFromPdf(byte[] pdfBytes)
    {
        // Load the PDF bytes into a memory stream.
        using var pdfStream = new MemoryStream(pdfBytes);

        // Use Aspose.Pdf.Facades.PdfExtractor to pull out the text.
        using var extractor = new PdfExtractor();
        extractor.BindPdf(pdfStream);
        extractor.ExtractText();

        // Retrieve the extracted text into another memory stream.
        using var textStream = new MemoryStream();
        extractor.GetText(textStream);

        // Aspose returns Unicode text by default.
        return Encoding.Unicode.GetString(textStream.ToArray());
    }
}