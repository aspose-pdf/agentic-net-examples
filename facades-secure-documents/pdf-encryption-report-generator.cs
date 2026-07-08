using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Aspose.Pdf.Facades;

class EncryptionReportGenerator
{
    static void Main()
    {
        // Folder containing PDF files to analyze
        const string inputFolder = "PdfCollection";
        // Output report file
        const string reportPath = "encryption_report.txt";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Gather all PDF files in the folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        var reportLines = new List<string>();
        // Header for the CSV‑style report
        reportLines.Add("FileName,IsEncrypted,EncryptionAlgorithm");

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load PDF metadata using PdfFileInfo (facade API)
                using (PdfFileInfo info = new PdfFileInfo(pdfPath))
                {
                    bool isEncrypted = info.IsEncrypted;
                    string algorithm = "None";

                    if (isEncrypted)
                    {
                        // Try to obtain the encryption algorithm via reflection – the property name
                        // differs between Aspose.Pdf versions (EncryptionAlgorithmName, EncryptionAlgorithm, etc.).
                        // If none are found we fall back to "Unknown".
                        algorithm = GetEncryptionAlgorithm(info);
                    }

                    string fileName = Path.GetFileName(pdfPath);
                    reportLines.Add($"{fileName},{isEncrypted},{algorithm}");
                    Console.WriteLine($"Processed: {fileName} | Encrypted: {isEncrypted} | Algorithm: {algorithm}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
                // Record the error in the report
                string fileName = Path.GetFileName(pdfPath);
                reportLines.Add($"{fileName},Error,{ex.GetType().Name}");
            }
        }

        // Write the report to the output file
        try
        {
            File.WriteAllLines(reportPath, reportLines);
            Console.WriteLine($"Encryption report saved to '{reportPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write report: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves the encryption algorithm name from a PdfFileInfo instance using reflection.
    /// Handles different Aspose.Pdf versions gracefully.
    /// </summary>
    private static string GetEncryptionAlgorithm(PdfFileInfo info)
    {
        // Preferred property (newer SDKs): EncryptionAlgorithmName (string)
        PropertyInfo prop = typeof(PdfFileInfo).GetProperty("EncryptionAlgorithmName", BindingFlags.Public | BindingFlags.Instance);
        if (prop != null)
        {
            try
            {
                var value = prop.GetValue(info);
                return value?.ToString() ?? "Unknown";
            }
            catch
            {
                // ignore and try next fallback
            }
        }

        // Older SDKs expose an enum property named EncryptionAlgorithm
        prop = typeof(PdfFileInfo).GetProperty("EncryptionAlgorithm", BindingFlags.Public | BindingFlags.Instance);
        if (prop != null)
        {
            try
            {
                var value = prop.GetValue(info);
                return value?.ToString() ?? "Unknown";
            }
            catch
            {
                // ignore and fall back
            }
        }

        // If neither property exists, we cannot determine the algorithm.
        return "Unknown";
    }
}
