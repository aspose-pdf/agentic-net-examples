using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to analyze
        const string inputFolder = @"C:\PdfFiles";
        // Path of the summary CSV file to generate
        const string summaryPath = @"C:\PdfFiles\encryption_summary.csv";

        // Ensure the input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Get all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        // Create (or overwrite) the summary file
        using (var writer = new StreamWriter(summaryPath, false))
        {
            // Write CSV header
            writer.WriteLine("FileName,IsEncrypted,EncryptionAlgorithm,PrivilegeConfiguration");

            foreach (string pdfPath in pdfFiles)
            {
                // Use PdfFileInfo to read metadata; it implements IDisposable
                using (var fileInfo = new PdfFileInfo(pdfPath))
                {
                    // Encryption status
                    bool isEncrypted = fileInfo.IsEncrypted;

                    // Aspose.Pdf.Facades does not expose the encryption algorithm directly.
                    // If the file is encrypted we report "Unknown", otherwise "None".
                    string algorithm = isEncrypted ? "Unknown" : "None";

                    // Retrieve the privilege configuration (may be null for unencrypted files)
                    DocumentPrivilege privilege = fileInfo.GetDocumentPrivilege();

                    // Convert privilege settings to a readable string.
                    // The DocumentPrivilege class overrides ToString() to list enabled permissions.
                    string privilegeDesc = privilege != null ? privilege.ToString() : "N/A";

                    // Write a line for this PDF
                    writer.WriteLine($"{Path.GetFileName(pdfPath)},{isEncrypted},{algorithm},\"{privilegeDesc}\"");
                }
            }
        }

        Console.WriteLine($"Encryption summary written to: {summaryPath}");
    }
}