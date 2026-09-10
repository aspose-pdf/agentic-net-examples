using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class PdfBatchValidator
{
    static void Main()
    {
        // Base directory of the application (works for both Windows and Linux)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Folder containing PDFs to validate – fall back to the current directory if it does not exist
        string inputFolder = Path.Combine(baseDir, "pdfs");
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder not found: {inputFolder}. Using current working directory instead.");
            inputFolder = Directory.GetCurrentDirectory();
        }

        // Folder where XML validation logs will be stored – ensure it exists
        string logFolder = Path.Combine(baseDir, "validation_logs");
        Directory.CreateDirectory(logFolder);

        // Store validation results for each file
        var results = new List<(string FileName, bool IsValid)>();

        try
        {
            // Iterate over all PDF files in the input folder
            foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
            {
                string fileName = Path.GetFileName(pdfPath);
                string logPath = Path.Combine(logFolder, Path.ChangeExtension(fileName, ".xml"));

                // Load the PDF document (lifecycle rule: use using for deterministic disposal)
                using (Document doc = new Document(pdfPath))
                {
                    // Validate against PDF/A-1B format and write XML log
                    bool isValid = doc.Validate(logPath, PdfFormat.PDF_A_1B);
                    results.Add((fileName, isValid));
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[Error] {ex.Message}");
            return;
        }

        // Compute overall compliance statistics
        int totalFiles = results.Count;
        int compliantFiles = 0;
        foreach (var r in results)
        {
            if (r.IsValid) compliantFiles++;
        }

        double compliancePercent = totalFiles > 0
            ? (double)compliantFiles / totalFiles * 100
            : 0.0;

        // Simple console dashboard
        Console.WriteLine("PDF Validation Dashboard");
        Console.WriteLine(new string('-', 30));
        Console.WriteLine($"Total files processed : {totalFiles}");
        Console.WriteLine($"Compliant files       : {compliantFiles}");
        Console.WriteLine($"Compliance percentage : {compliancePercent:F2}%");
        Console.WriteLine();
        Console.WriteLine("Per‑file results:");
        foreach (var r in results)
        {
            Console.WriteLine($"{r.FileName}: {(r.IsValid ? "PASS" : "FAIL")}");
        }
    }
}
