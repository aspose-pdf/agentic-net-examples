using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Aspose.Pdf; // Aspose.Pdf contains Document, DocSaveOptions, etc.

class Program
{
    static void Main()
    {
        // Paths – adjust as needed.
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";
        const string reportJsonPath = "conversion_report.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Prepare a stopwatch to measure conversion time.
        Stopwatch sw = new Stopwatch();

        // Load the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Gather pre‑conversion statistics.
            long inputFileSize = new FileInfo(inputPdfPath).Length;
            int pageCount = pdfDoc.Pages.Count;

            // Configure DOCX save options (must pass explicit SaveOptions for non‑PDF output).
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Save as DOCX format.
                Format = DocSaveOptions.DocFormat.DocX,
                // Use the Flow recognition mode for better editability (optional).
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Enable bullet recognition (optional).
                RecognizeBullets = true
            };

            // Perform the conversion and measure its duration.
            sw.Start();
            pdfDoc.Save(outputDocxPath, saveOptions);
            sw.Stop();

            // Gather post‑conversion statistics.
            long outputFileSize = new FileInfo(outputDocxPath).Length;

            // Build a report object.
            var report = new
            {
                InputFile = Path.GetFullPath(inputPdfPath),
                OutputFile = Path.GetFullPath(outputDocxPath),
                InputFileSizeBytes = inputFileSize,
                OutputFileSizeBytes = outputFileSize,
                PageCount = pageCount,
                ConversionTimeMilliseconds = sw.ElapsedMilliseconds,
                ConversionSucceeded = File.Exists(outputDocxPath)
            };

            // Serialize the report to indented JSON.
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string jsonReport = JsonSerializer.Serialize(report, jsonOptions);

            // Write the JSON report to disk.
            File.WriteAllText(reportJsonPath, jsonReport);

            Console.WriteLine($"Conversion completed. Report written to '{reportJsonPath}'.");
        }
    }
}