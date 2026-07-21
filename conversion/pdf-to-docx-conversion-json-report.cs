using System;
using System.Diagnostics;
using System.IO;
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

        var report = new ConversionReport
        {
            InputFile = inputPdfPath,
            OutputFile = outputDocxPath,
            InputFileSizeBytes = new FileInfo(inputPdfPath).Length
        };

        var stopwatch = Stopwatch.StartNew();

        // Load PDF and convert to DOCX.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Input statistics.
            report.InputPageCount = pdfDocument.Pages.Count;

            // Configure DOCX conversion options.
            var docSaveOptions = new DocSaveOptions
            {
                // Correct enum value for DOCX format.
                Format = DocSaveOptions.DocFormat.DocX,
                // Enable bullet detection.
                RecognizeBullets = true
            };

            // Save the converted document.
            pdfDocument.Save(outputDocxPath, docSaveOptions);
        }

        stopwatch.Stop();

        // Output statistics.
        report.OutputFileSizeBytes = new FileInfo(outputDocxPath).Length;
        // DOCX does not expose page count; use input page count as approximation.
        report.OutputPageCount = report.InputPageCount;
        report.ConversionTimeMs = stopwatch.ElapsedMilliseconds;

        // Serialize report to JSON.
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(report, jsonOptions);
        File.WriteAllText(reportJsonPath, json);

        Console.WriteLine($"Conversion completed. Report saved to '{reportJsonPath}'.");
    }

    // DTO for JSON serialization.
    private class ConversionReport
    {
        public string InputFile { get; set; }
        public string OutputFile { get; set; }
        public long InputFileSizeBytes { get; set; }
        public long OutputFileSizeBytes { get; set; }
        public int InputPageCount { get; set; }
        public int OutputPageCount { get; set; }
        public long ConversionTimeMs { get; set; }

        // Parameterless constructor ensures non‑nullable string properties are initialized.
        public ConversionReport()
        {
            InputFile = string.Empty;
            OutputFile = string.Empty;
        }
    }
}