using System;
using System.IO;
using System.Collections.Generic;
using System.Linq; // Added for Any()
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to process
        const string inputFolder = @"C:\PdfBatch";
        // Path for the generated summary report (CSV format)
        const string reportPath = @"C:\PdfBatch\FieldSummaryReport.csv";

        // Verify the input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Collect all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        // Prepare a list of CSV lines; first line is the header
        List<string> reportLines = new List<string>
        {
            "FileName,FieldName,FieldValue"
        };

        // Process each PDF file
        foreach (string pdfPath in pdfFiles)
        {
            // Ensure the file exists (defensive check)
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found (skipped): {pdfPath}");
                continue;
            }

            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // If the document has no form fields, note it and continue
                if (doc.Form == null || doc.Form.Fields == null || !doc.Form.Fields.Any())
                {
                    reportLines.Add($"{Path.GetFileName(pdfPath)},(NoFields),");
                    continue;
                }

                // Iterate over all form fields and record their names and values
                foreach (var field in doc.Form.Fields)
                {
                    // Guard against unexpected nulls (field itself is never null in the collection)
                    string fieldName = field?.FullName ?? string.Empty;
                    string fieldValue = field?.Value?.ToString() ?? string.Empty;

                    // Escape commas and double quotes in values to keep CSV integrity
                    if (fieldValue.Contains(",") || fieldValue.Contains('"'))
                    {
                        fieldValue = $"\"{fieldValue.Replace("\"", "\"\"")}\"";
                    }

                    reportLines.Add($"{Path.GetFileName(pdfPath)},{fieldName},{fieldValue}");
                }
            }
        }

        // Write the report to disk
        try
        {
            File.WriteAllLines(reportPath, reportLines);
            Console.WriteLine($"Field summary report generated at: {reportPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write report: {ex.Message}");
        }
    }
}
