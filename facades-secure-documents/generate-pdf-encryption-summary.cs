using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing PDF files to analyze
        const string inputFolder = @"C:\PdfFolder";
        // Path of the summary file to generate
        const string summaryPath = @"C:\PdfFolder\encryption_summary.csv";

        // Prepare the summary file with a header line
        using (StreamWriter writer = new StreamWriter(summaryPath, false))
        {
            writer.WriteLine("FileName,IsEncrypted,Algorithm,Privileges");
        }

        // Enumerate all PDF files in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Use PdfFileInfo to retrieve encryption status and privileges
            using (PdfFileInfo fileInfo = new PdfFileInfo(pdfPath))
            {
                bool isEncrypted = fileInfo.IsEncrypted;
                // Privilege configuration (e.g., Print, Modify, etc.)
                DocumentPrivilege privileges = fileInfo.GetDocumentPrivilege();

                // Algorithm information is not exposed via PdfFileInfo.
                // When encrypted, we record it as "Unknown" because Facades API does not provide a getter.
                string algorithm = isEncrypted ? "Unknown" : "None";

                // Append the information to the summary CSV
                using (StreamWriter writer = new StreamWriter(summaryPath, true))
                {
                    writer.WriteLine($"{Path.GetFileName(pdfPath)},{isEncrypted},{algorithm},{privileges}");
                }
            }
        }

        Console.WriteLine($"Encryption summary written to '{summaryPath}'.");
    }
}