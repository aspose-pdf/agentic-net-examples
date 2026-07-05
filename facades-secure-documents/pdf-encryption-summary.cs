using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class PdfEncryptionSummary
{
    static void Main()
    {
        // Folder containing PDFs to analyze
        const string inputFolder = @"C:\PdfFiles";
        // Output CSV file with the summary
        const string outputCsv = @"C:\PdfFiles\encryption_summary.csv";

        // Collect summary lines
        var lines = new List<string>();
        // Header row
        lines.Add("FileName,IsEncrypted,Algorithm,Privileges");

        // Enumerate all PDF files in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Use PdfFileInfo (facade) to read encryption info
            using (PdfFileInfo info = new PdfFileInfo(pdfPath))
            {
                bool isEncrypted = info.IsEncrypted;
                // Algorithm is not exposed via the API; mark as N/A
                string algorithm = "N/A";

                // Get privilege configuration (DocumentPrivilege enum)
                DocumentPrivilege privilege = info.GetDocumentPrivilege();

                // Build CSV line
                string line = string.Format("{0},{1},{2},{3}",
                    Path.GetFileName(pdfPath),
                    isEncrypted,
                    algorithm,
                    privilege);

                lines.Add(line);
            }
        }

        // Write all lines to the CSV file
        File.WriteAllLines(outputCsv, lines);
        Console.WriteLine($"Encryption summary saved to '{outputCsv}'.");
    }
}