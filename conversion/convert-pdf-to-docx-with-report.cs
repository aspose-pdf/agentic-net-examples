using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf; // Document, DocSaveOptions, etc.

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocxPath = "output.docx";
        const string reportJsonPath = "conversion_report.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block (ensures disposal).
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Gather basic statistics before conversion.
                int pageCount = pdfDoc.Pages.Count;

                // Configure DOCX save options (non‑PDF format requires explicit options).
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Output format: DOCX.
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use Flow mode for better editability (optional).
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Enable bullet recognition (optional).
                    RecognizeBullets = true
                };

                // Convert and save as DOCX.
                pdfDoc.Save(outputDocxPath, saveOptions);

                // Build a simple report object.
                var report = new
                {
                    InputFile   = inputPdfPath,
                    OutputFile  = outputDocxPath,
                    Pages       = pageCount,
                    Success     = true,
                    TimestampUtc = DateTime.UtcNow
                };

                // Serialize report to indented JSON.
                string json = JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON report to file.
                File.WriteAllText(reportJsonPath, json);

                Console.WriteLine($"Conversion completed. Report saved to '{reportJsonPath}'.");
            }
        }
        catch (Exception ex)
        {
            // In case of any error, generate a failure report.
            var errorReport = new
            {
                InputFile   = inputPdfPath,
                OutputFile  = outputDocxPath,
                Success     = false,
                ErrorMessage = ex.Message,
                TimestampUtc = DateTime.UtcNow
            };

            string errorJson = JsonSerializer.Serialize(errorReport, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(reportJsonPath, errorJson);

            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}