using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, PdfFormat, etc.

class PdfBatchValidator
{
    // Entry point
    static void Main()
    {
        // Input directory containing PDFs to validate
        const string inputDirectory = @"C:\PdfFiles";

        // Directory where XML validation logs will be stored
        const string logDirectory = @"C:\PdfValidationLogs";

        // Path of the summary CSV file
        const string summaryCsvPath = @"C:\PdfValidationSummary.csv";

        // Ensure the log directory exists
        Directory.CreateDirectory(logDirectory);

        // Prepare a list to hold summary records
        var summaryRecords = new List<string>();
        // Add CSV header
        summaryRecords.Add("FileName,IsCompliant,LogFilePath");

        // Enumerate all PDF files in the input directory (non‑recursive)
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            // Determine log file name (same base name with .xml extension)
            string logFileName = Path.GetFileNameWithoutExtension(pdfPath) + "_validation.xml";
            string logFilePath = Path.Combine(logDirectory, logFileName);

            bool isCompliant = false;

            // Load the PDF document and validate it
            // Document implements IDisposable, so wrap it in a using block
            using (Document doc = new Document(pdfPath))
            {
                // Validate against PDF/A‑1b (you can change the format as needed)
                // The Validate method writes an XML log to the specified file
                isCompliant = doc.Validate(logFilePath, PdfFormat.PDF_A_1B);
            }

            // Build a CSV line for this file
            string csvLine = $"{Path.GetFileName(pdfPath)},{isCompliant},{logFilePath}";
            summaryRecords.Add(csvLine);
        }

        // Write all summary lines to the CSV file (UTF‑8 encoding)
        File.WriteAllLines(summaryCsvPath, summaryRecords, Encoding.UTF8);

        Console.WriteLine($"Validation completed. Summary CSV written to: {summaryCsvPath}");
    }
}