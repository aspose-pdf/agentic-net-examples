using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to validate
        const string inputFolder = "pdfs";
        // Folder where individual XML validation logs will be stored
        const string logFolder = "validation_logs";
        // Path for the dashboard (CSV format)
        const string dashboardPath = "dashboard.csv";

        // Ensure the log folder exists
        Directory.CreateDirectory(logFolder);

        // Verify that the input folder exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. No files to process.");
            return;
        }

        // Collect validation results for each file
        var results = new List<(string FileName, bool IsCompliant)>();

        // Iterate over all PDF files in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            // XML log file name matches the PDF name
            string logPath = Path.Combine(logFolder, Path.ChangeExtension(fileName, ".xml"));

            // Load the PDF document (using rule: wrap in using)
            using (Document doc = new Document(pdfPath))
            {
                // Validate against PDF/A‑1B. The Validate method writes an XML log.
                // In recent Aspose.Pdf versions the method returns void, so we parse the log.
                doc.Validate(logPath, PdfFormat.PDF_A_1B);

                // Determine compliance by inspecting the generated XML log.
                // The log contains an <IsCompliant> element with value "true" or "false".
                bool isCompliant = false;
                if (File.Exists(logPath))
                {
                    string logContent = File.ReadAllText(logPath);
                    // Simple check – look for the string "<IsCompliant>true" (case‑insensitive)
                    isCompliant = logContent.IndexOf("<IsCompliant>true", StringComparison.OrdinalIgnoreCase) >= 0;
                }
                results.Add((fileName, isCompliant));
            }
        }

        // Generate a simple CSV dashboard showing compliance percentage per file
        using (StreamWriter writer = new StreamWriter(dashboardPath, false))
        {
            writer.WriteLine("FileName,CompliancePercentage");
            foreach (var result in results)
            {
                int percent = result.IsCompliant ? 100 : 0;
                writer.WriteLine($"{result.FileName},{percent}");
            }
        }

        Console.WriteLine($"Validation completed. Dashboard saved to '{dashboardPath}'.");
    }
}
