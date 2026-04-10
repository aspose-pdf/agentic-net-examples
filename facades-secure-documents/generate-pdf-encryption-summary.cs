using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF files to analyze
        string[] pdfFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        // Output summary file (CSV format)
        string summaryPath = "encryption_summary.csv";

        // Create (or overwrite) the summary file
        using (StreamWriter writer = new StreamWriter(summaryPath, false))
        {
            // Header: File name, IsEncrypted flag, Encryption algorithm (N/A if unknown), Privilege settings
            writer.WriteLine("File,IsEncrypted,Algorithm,Privileges");

            foreach (string pdfPath in pdfFiles)
            {
                if (!File.Exists(pdfPath))
                {
                    Console.Error.WriteLine($"File not found: {pdfPath}");
                    continue;
                }

                // Load PDF meta‑information using the Facades PdfFileInfo class
                using (PdfFileInfo info = new PdfFileInfo(pdfPath))
                {
                    bool isEncrypted = info.IsEncrypted;                     // true if the PDF is encrypted
                    // The Facades API does not expose the encryption algorithm directly;
                    // we record "N/A" when the algorithm cannot be determined.
                    string algorithm = "N/A";

                    // Retrieve the privilege configuration (e.g., Print, Modify, etc.)
                    var privilege = info.GetDocumentPrivilege();

                    // Write a line for this PDF
                    writer.WriteLine($"{Path.GetFileName(pdfPath)},{isEncrypted},{algorithm},{privilege}");
                }
            }
        }

        Console.WriteLine($"Encryption summary written to '{summaryPath}'.");
    }
}