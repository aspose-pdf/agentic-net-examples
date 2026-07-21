using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf; // Core Aspose.Pdf namespace (no Facades)

class Program
{
    static void Main()
    {
        // Input directory containing PDF files to validate
        const string inputDirectory = @"C:\PdfFiles";

        // Directory where individual XML validation logs will be saved
        const string logDirectory = @"C:\PdfValidationLogs";

        // Path for the summary CSV file
        const string summaryCsvPath = @"C:\PdfValidationSummary.csv";

        // Ensure the log directory exists
        Directory.CreateDirectory(logDirectory);

        // Prepare CSV header
        var csvLines = new List<string>
        {
            "FileName,IsValid,LogFile"
        };

        // Get all PDF files in the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            string fileName = Path.GetFileName(pdfPath);
            string logFilePath = Path.Combine(logDirectory,
                Path.GetFileNameWithoutExtension(pdfPath) + ".xml");

            bool isValid = false;

            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Validate against PDF/A‑1B (you can choose another PdfFormat if needed)
                    // The method returns true if validation succeeds, false otherwise.
                    isValid = doc.Validate(logFilePath, PdfFormat.PDF_A_1B);
                }
            }
            catch (Exception ex)
            {
                // If an exception occurs, treat the document as invalid and record the error
                isValid = false;
                // Write a simple error log (optional)
                File.WriteAllText(logFilePath,
                    $"<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<Error>{ex.Message}</Error>");
            }

            // Add a line to the CSV summary
            csvLines.Add($"{fileName},{isValid},{logFilePath}");
        }

        // Write the summary CSV file
        File.WriteAllLines(summaryCsvPath, csvLines);

        Console.WriteLine($"Validation complete. Summary written to '{summaryCsvPath}'.");
    }
}