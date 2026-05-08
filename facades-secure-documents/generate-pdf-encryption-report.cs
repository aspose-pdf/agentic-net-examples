using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to be inspected
        const string inputFolder = @"C:\PdfCollection";
        // Path for the generated usage‑report
        const string reportPath = @"C:\PdfCollection\EncryptionReport.txt";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Gather all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        var reportLines = new List<string>
        {
            "FileName\tIsEncrypted\tEncryptionAlgorithm"
        };

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load file information using the Facades API
                using (PdfFileInfo fileInfo = new PdfFileInfo(pdfPath))
                {
                    bool isEncrypted = fileInfo.IsEncrypted;
                    // Aspose.Pdf.Facades does not expose the encryption algorithm directly.
                    // If the file is encrypted we report "Unknown"; otherwise "None".
                    string algorithm = isEncrypted ? "Unknown" : "None";

                    string fileName = Path.GetFileName(pdfPath);
                    reportLines.Add($"{fileName}\t{isEncrypted}\t{algorithm}");
                }
            }
            catch (Exception ex)
            {
                // Record any errors encountered while processing a file
                string fileName = Path.GetFileName(pdfPath);
                reportLines.Add($"{fileName}\tError\t{ex.Message}");
            }
        }

        // Write the usage‑report to the specified file
        try
        {
            File.WriteAllLines(reportPath, reportLines);
            Console.WriteLine($"Encryption report generated at: {reportPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write report: {ex.Message}");
        }
    }
}