using System;
using System.IO;
using System.Diagnostics;
using System.Text.Json;
using Aspose.Pdf;

class PdfToDocxConverter
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
        ConversionStats stats = new ConversionStats();

        try
        {
            // Load PDF and record page count
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                stats.PageCount = pdfDoc.Pages.Count;

                // Measure conversion time
                var sw = Stopwatch.StartNew();

                // Save as DOCX using explicit DocSaveOptions
                DocSaveOptions docOptions = new DocSaveOptions {
                    Format = DocSaveOptions.DocFormat.DocX
                };
                pdfDoc.Save(outputDocxPath, docOptions);

                sw.Stop();
                stats.ConversionTimeMs = sw.ElapsedMilliseconds;
            }

            // Record output file size
            FileInfo outputInfo = new FileInfo(outputDocxPath);
            stats.OutputFileSizeBytes = outputInfo.Length;
            stats.Success = true;
        }
        catch (Exception ex)
        {
            stats.Success = false;
            stats.ErrorMessage = ex.Message;
        }

        // Serialize statistics to JSON
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(stats, jsonOptions);
        File.WriteAllText(reportJsonPath, json);

        Console.WriteLine($"Conversion report written to {reportJsonPath}");
    }

    // Simple POCO for JSON serialization
    private class ConversionStats
    {
        public int PageCount { get; set; }
        public long ConversionTimeMs { get; set; }
        public long OutputFileSizeBytes { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}