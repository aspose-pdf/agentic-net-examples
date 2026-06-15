using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class PdfValidationDashboard
{
    static void Main(string[] args)
    {
        // Input folder containing PDF files to validate
        string inputFolder = args.Length > 0 ? args[0] : "InputPdfs";
        // Folder where individual XML validation logs will be saved
        string logFolder = Path.Combine(inputFolder, "ValidationLogs");
        Directory.CreateDirectory(logFolder);

        // Collect results for the dashboard
        var results = new List<(string FileName, bool IsCompliant)>();

        // Iterate over all PDF files in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string logPath = Path.Combine(logFolder, Path.GetFileNameWithoutExtension(pdfPath) + "_log.xml");

            // Load the PDF document using a using block (lifecycle rule)
            using (Document doc = new Document(pdfPath))
            {
                // Validate against PDF/UA (Universal Accessibility) standard.
                // The Validate method writes an XML log to the specified path and returns true if compliant.
                bool isCompliant = doc.Validate(logPath, PdfFormat.PDF_UA_1);
                results.Add((fileName, isCompliant));
            }
        }

        // Generate console dashboard
        Console.WriteLine("PDF Validation Dashboard");
        Console.WriteLine(new string('=', 30));
        int total = results.Count;
        int compliantCount = 0;

        foreach (var result in results)
        {
            string status = result.IsCompliant ? "Compliant" : "Non‑Compliant";
            Console.WriteLine($"{result.FileName}: {status}");
            if (result.IsCompliant) compliantCount++;
        }

        Console.WriteLine(new string('-', 30));
        double compliancePercentage = total > 0 ? (double)compliantCount / total * 100 : 0;
        Console.WriteLine($"Total files processed: {total}");
        Console.WriteLine($"Compliant files: {compliantCount}");
        Console.WriteLine($"Compliance percentage: {compliancePercentage:F2}%");
    }
}