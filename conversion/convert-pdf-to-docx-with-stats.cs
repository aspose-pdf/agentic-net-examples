using System;
using System.IO;
using System.Diagnostics;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
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

        // Prepare statistics container
        ConversionStats stats = new ConversionStats {
            InputFile = inputPdfPath,
            OutputFile = outputDocxPath,
            InputSizeBytes = new FileInfo(inputPdfPath).Length
        };

        var stopwatch = Stopwatch.StartNew();

        // Load PDF and convert to DOCX
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            stats.PageCount = pdfDoc.Pages.Count;

            DocSaveOptions docOptions = new DocSaveOptions {
                Format = DocSaveOptions.DocFormat.DocX
            };

            pdfDoc.Save(outputDocxPath, docOptions);
        }

        stopwatch.Stop();
        stats.ConversionTimeMs = stopwatch.ElapsedMilliseconds;

        // Gather output file info
        if (File.Exists(outputDocxPath))
        {
            stats.OutputSizeBytes = new FileInfo(outputDocxPath).Length;
        }

        // Serialize statistics to JSON
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string jsonReport = JsonSerializer.Serialize(stats, jsonOptions);
        File.WriteAllText(reportJsonPath, jsonReport);

        Console.WriteLine($"Conversion completed. Report written to {reportJsonPath}");
    }

    // Simple DTO for JSON serialization
    private class ConversionStats
    {
        public string InputFile { get; set; }
        public string OutputFile { get; set; }
        public long InputSizeBytes { get; set; }
        public long OutputSizeBytes { get; set; }
        public int PageCount { get; set; }
        public long ConversionTimeMs { get; set; }
    }
}