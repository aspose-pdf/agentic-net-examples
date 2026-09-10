using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

namespace PdfToDocxConversion
{
    // Simple class to hold conversion statistics.
    public class ConversionStats
    {
        public string InputFile { get; set; }
        public string OutputFile { get; set; }
        public long InputSizeBytes { get; set; }
        public long OutputSizeBytes { get; set; }
        public int PageCount { get; set; }
        public long ConversionTimeMs { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string inputPdfPath = "input.pdf";
            const string outputDocxPath = "output.docx";

            ConversionStats stats = new ConversionStats {
                InputFile = inputPdfPath,
                OutputFile = outputDocxPath,
                Success = false,
                Message = "Conversion failed."
            };

            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
                stats.Message = "Input file not found.";
                OutputStats(stats);
                return;
            }

            try
            {
                // Record input file size.
                stats.InputSizeBytes = new FileInfo(inputPdfPath).Length;

                var stopwatch = Stopwatch.StartNew();

                // Load the PDF document.
                using (Document pdfDocument = new Document(inputPdfPath))
                {
                    // Capture page count.
                    stats.PageCount = pdfDocument.Pages.Count;

                    // Configure DOCX save options.
                    DocSaveOptions saveOptions = new DocSaveOptions {
                        // Save as DOCX format.
                        Format = DocSaveOptions.DocFormat.DocX,
                        // Use Flow mode for better editability.
                        Mode = DocSaveOptions.RecognitionMode.Flow,
                        // Optional: enable bullet recognition.
                        RecognizeBullets = true
                    };

                    // Save the document as DOCX.
                    pdfDocument.Save(outputDocxPath, saveOptions);
                }

                stopwatch.Stop();
                stats.ConversionTimeMs = stopwatch.ElapsedMilliseconds;

                // Record output file size.
                if (File.Exists(outputDocxPath))
                {
                    stats.OutputSizeBytes = new FileInfo(outputDocxPath).Length;
                    stats.Success = true;
                    stats.Message = "Conversion succeeded.";
                }
                else
                {
                    stats.Message = "Output file was not created.";
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during conversion: {ex.Message}");
                stats.Message = $"Exception: {ex.Message}";
            }

            OutputStats(stats);
        }

        // Serializes the statistics to JSON and writes to console.
        private static void OutputStats(ConversionStats stats)
        {
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(stats, jsonOptions);
            Console.WriteLine(json);
        }
    }
}