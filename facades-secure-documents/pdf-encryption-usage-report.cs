using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to analyze
        const string inputFolder = @"C:\PdfCollection";
        // Path for the generated usage‑report
        const string reportPath = @"C:\PdfCollection\EncryptionReport.txt";

        // Verify that the input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the directory for the report exists (in case the folder was created later)
        string reportDir = Path.GetDirectoryName(reportPath);
        if (!string.IsNullOrEmpty(reportDir) && !Directory.Exists(reportDir))
        {
            Directory.CreateDirectory(reportDir);
        }

        // Collect all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        // Prepare a list to hold report lines
        var reportLines = new List<string>();
        reportLines.Add("Encryption Algorithm Usage Report");
        reportLines.Add($"Generated on: {DateTime.Now}");
        reportLines.Add(string.Empty);
        reportLines.Add("File Name | Encrypted | Algorithm");
        reportLines.Add(new string('-', 60));

        foreach (string pdfPath in pdfFiles)
        {
            // Use PdfFileInfo (Facade) to inspect the PDF
            using (PdfFileInfo info = new PdfFileInfo(pdfPath))
            {
                bool isEncrypted = info.IsEncrypted;
                string algorithm = "N/A";

                if (isEncrypted)
                {
                    // Aspose.Pdf.Facades does not expose the exact algorithm.
                    // We note that the file is encrypted; algorithm details are unavailable.
                    algorithm = "Encrypted (algorithm not exposed)";
                }
                else
                {
                    algorithm = "None (plain PDF)";
                }

                string fileName = Path.GetFileName(pdfPath);
                reportLines.Add($"{fileName} | {isEncrypted} | {algorithm}");
            }
        }

        // Write the report to a text file
        File.WriteAllLines(reportPath, reportLines);

        Console.WriteLine($"Encryption report generated at: {reportPath}");
    }
}
