using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for any text-related options if needed

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

        ConversionReport report = new ConversionReport {
            InputFile = inputPdfPath,
            OutputFile = outputDocxPath,
            StartTimeUtc = DateTime.UtcNow
        };

        var stopwatch = Stopwatch.StartNew();

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Gather some source statistics
                report.SourcePageCount = pdfDocument.Pages.Count;
                report.SourceFileSizeBytes = new FileInfo(inputPdfPath).Length;

                // Prepare DOCX save options
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Save as DOCX
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use Flow mode for better editability
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Optional: enable bullet recognition
                    RecognizeBullets = true
                };

                // Save as DOCX
                pdfDocument.Save(outputDocxPath, saveOptions);
            }

            // Gather output statistics
            report.Success = true;
            report.OutputFileSizeBytes = new FileInfo(outputDocxPath).Length;
        }
        catch (Exception ex)
        {
            report.Success = false;
            report.ErrorMessage = ex.Message;
        }
        finally
        {
            stopwatch.Stop();
            report.DurationMilliseconds = stopwatch.ElapsedMilliseconds;
            report.EndTimeUtc = DateTime.UtcNow;
        }

        // Serialize report to JSON
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(report, jsonOptions);
        File.WriteAllText(reportJsonPath, json);

        Console.WriteLine($"Conversion report written to '{reportJsonPath}'.");
    }

    // Simple DTO for the conversion report
    private class ConversionReport
    {
        public string InputFile { get; set; }
        public string OutputFile { get; set; }
        public int SourcePageCount { get; set; }
        public long SourceFileSizeBytes { get; set; }
        public long OutputFileSizeBytes { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public long DurationMilliseconds { get; set; }
        public DateTime StartTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }
    }
}