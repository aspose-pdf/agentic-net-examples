using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Aspose.Pdf; // Document, DocSaveOptions, etc.

class Program
{
    // Simple DTO for conversion statistics.
    private class ConversionStats
    {
        public string InputFile { get; set; }
        public string OutputFile { get; set; }
        public long InputFileSizeBytes { get; set; }
        public long OutputFileSizeBytes { get; set; }
        public int PageCount { get; set; }
        public long ConversionTimeMs { get; set; }
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";
        const string reportJsonPath = "conversion_report.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        var stopwatch = Stopwatch.StartNew();

        // Load the PDF document.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Prepare DOCX save options.
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Save as DOCX format.
                Format = DocSaveOptions.DocFormat.DocX,
                // Use Flow mode for better editability (optional).
                Mode = DocSaveOptions.RecognitionMode.Flow
            };

            // Save the document as DOCX.
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        stopwatch.Stop();

        // Gather statistics.
        ConversionStats stats = new ConversionStats {
            InputFile = inputPdfPath,
            OutputFile = outputDocxPath,
            InputFileSizeBytes = new FileInfo(inputPdfPath).Length,
            OutputFileSizeBytes = new FileInfo(outputDocxPath).Length,
            PageCount = GetPageCount(inputPdfPath),
            ConversionTimeMs = stopwatch.ElapsedMilliseconds
        };

        // Serialize statistics to JSON.
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(stats, jsonOptions);
        File.WriteAllText(reportJsonPath, json);

        Console.WriteLine($"Conversion completed. Report saved to '{reportJsonPath}'.");
    }

    // Helper to retrieve page count without keeping the document open.
    private static int GetPageCount(string pdfPath)
    {
        using (Document doc = new Document(pdfPath))
        {
            return doc.Pages.Count;
        }
    }
}