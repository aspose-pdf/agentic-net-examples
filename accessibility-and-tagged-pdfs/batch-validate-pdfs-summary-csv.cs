using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class PdfBatchValidator
{
    static void Main()
    {
        // Input directory containing PDF files
        const string inputDirectory = @"C:\PdfFiles";
        // Directory where XML validation logs will be stored
        const string logDirectory = @"C:\PdfValidationLogs";
        // Path of the summary CSV file
        const string summaryCsvPath = @"C:\PdfValidationSummary.csv";

        // Verify that the input directory exists; if not, inform the user and exit gracefully.
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Input directory '{inputDirectory}' does not exist. No files to validate.");
            return;
        }

        // Ensure the log directory exists
        Directory.CreateDirectory(logDirectory);

        // Prepare a list to hold summary records
        var summaryRecords = new List<string>();
        // Add CSV header
        summaryRecords.Add("FileName,IsValid,LogFilePath");

        // Iterate over all PDF files in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            // Determine the log file name (same base name with .xml extension)
            string logFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".xml";
            string logFilePath = Path.Combine(logDirectory, logFileName);

            bool isValid = false;

            // Load and validate the PDF inside a using block (ensures disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Validate against PDF/A-1b format; the method writes an XML log
                isValid = doc.Validate(logFilePath, PdfFormat.PDF_A_1B);
            }

            // Record the result for the CSV summary
            string record = $"{Path.GetFileName(pdfPath)},{isValid},{logFilePath}";
            summaryRecords.Add(record);
        }

        // Write all summary records to the CSV file
        File.WriteAllLines(summaryCsvPath, summaryRecords);
        Console.WriteLine($"Validation completed. Summary written to '{summaryCsvPath}'.");
    }
}
