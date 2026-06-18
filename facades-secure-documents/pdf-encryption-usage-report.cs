using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class PdfEncryptionReport
{
    static void Main()
    {
        // Folder containing PDF files to analyze
        const string pdfFolder = "PdfCollection";
        // Output report file
        const string reportPath = "EncryptionReport.txt";

        if (!Directory.Exists(pdfFolder))
        {
            Console.Error.WriteLine($"Folder not found: {pdfFolder}");
            return;
        }

        // Gather all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(pdfFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        var reportLines = new List<string>();
        reportLines.Add("PDF Encryption Usage Report");
        reportLines.Add($"Generated on: {DateTime.Now}");
        reportLines.Add(string.Empty);
        reportLines.Add("File Name | Encrypted | Encryption Algorithm / Privilege");
        reportLines.Add(new string('-', 80));

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load PDF metadata using PdfFileInfo (no full document load required)
                using (PdfFileInfo info = new PdfFileInfo(pdfPath))
                {
                    bool isEncrypted = info.IsEncrypted;
                    string algorithmInfo = "None";

                    if (isEncrypted)
                    {
                        // Retrieve document privilege which contains encryption details
                        // The exact algorithm name may be part of the privilege's string representation
                        var privilege = info.GetDocumentPrivilege();
                        algorithmInfo = privilege != null ? privilege.ToString() : "Unknown";
                    }

                    string fileName = Path.GetFileName(pdfPath);
                    reportLines.Add($"{fileName} | {(isEncrypted ? "Yes" : "No")} | {algorithmInfo}");
                }
            }
            catch (Exception ex)
            {
                // Record any errors encountered while processing a file
                string fileName = Path.GetFileName(pdfPath);
                reportLines.Add($"{fileName} | Error | {ex.Message}");
            }
        }

        // Write the report to the output file
        File.WriteAllLines(reportPath, reportLines);
        Console.WriteLine($"Encryption usage report saved to '{reportPath}'.");
    }
}