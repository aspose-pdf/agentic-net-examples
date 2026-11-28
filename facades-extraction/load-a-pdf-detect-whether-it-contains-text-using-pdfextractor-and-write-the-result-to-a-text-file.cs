using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "result.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // PdfExtractor works directly with a file path; no Document object is needed.
        var extractor = new PdfExtractor();
        extractor.BindPdf(inputPdfPath);
        extractor.ExtractText();

        // GetText has no parameter‑less overload. Provide a stream, then read the text.
        string extractedText;
        using (var ms = new MemoryStream())
        {
            extractor.GetText(ms); // writes extracted text into the stream
            ms.Position = 0;       // rewind for reading
            using var reader = new StreamReader(ms, Encoding.UTF8);
            extractedText = reader.ReadToEnd();
        }

        bool containsText = !string.IsNullOrWhiteSpace(extractedText);
        string resultLine = $"Contains text: {(containsText ? "Yes" : "No")}";
        File.WriteAllText(outputTxtPath, resultLine);

        Console.WriteLine($"Analysis complete. Result written to '{outputTxtPath}'.");
    }
}