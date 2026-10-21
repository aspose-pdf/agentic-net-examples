using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class EncryptionReportGenerator
{
    static void Main()
    {
        // Directory containing PDF files to analyze
        const string pdfFolder = @"C:\PdfCollection";
        // Output report file
        const string reportPath = @"C:\PdfCollection\encryption_report.txt";

        if (!Directory.Exists(pdfFolder))
        {
            Console.Error.WriteLine($"Folder not found: {pdfFolder}");
            return;
        }

        // Gather all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(pdfFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        // Prepare a StringBuilder for the report
        StringBuilder reportBuilder = new StringBuilder();
        reportBuilder.AppendLine("File Name,Encrypted,Encryption Algorithm");

        // Iterate over each PDF file
        foreach (string pdfPath in pdfFiles)
        {
            // Use PdfFileInfo (Facades) to read encryption details
            PdfFileInfo fileInfo = new PdfFileInfo(pdfPath);

            bool isEncrypted = fileInfo.IsEncrypted;
            // The PdfFileInfo class in older Aspose.Pdf versions does not expose the
            // EncryptionAlgorithm property. When the property is unavailable we fall back
            // to a generic placeholder. If you are using a newer version that provides the
            // property, you can uncomment the line below.
            // var algorithm = fileInfo.EncryptionAlgorithm; // CryptoAlgorithm enum
            // string algorithmName = isEncrypted ? algorithm.ToString() : "None";
            string algorithmName = isEncrypted ? "Encrypted (algorithm unknown)" : "None";

            // Append line to report
            string fileName = Path.GetFileName(pdfPath);
            reportBuilder.AppendLine($"{fileName},{isEncrypted},{algorithmName}");
        }

        // Write the report to disk
        try
        {
            File.WriteAllText(reportPath, reportBuilder.ToString(), Encoding.UTF8);
            Console.WriteLine($"Encryption report generated at: {reportPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write report: {ex.Message}");
        }
    }
}
