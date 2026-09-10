using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileInfo and PdfFileSecurity are in this namespace
using Aspose.Pdf;          // Document class for loading PDFs when needed

class Program
{
    static void Main()
    {
        // Directory containing PDFs to analyze
        const string pdfDirectory = @"C:\PdfFolder";
        // Output summary file path
        const string summaryPath = @"C:\PdfFolder\EncryptionSummary.txt";

        // Prepare the summary writer
        using (StreamWriter writer = new StreamWriter(summaryPath, false))
        {
            writer.WriteLine("PDF Encryption Summary");
            writer.WriteLine("======================");
            writer.WriteLine();

            // Enumerate all PDF files in the directory
            foreach (string pdfPath in Directory.GetFiles(pdfDirectory, "*.pdf"))
            {
                // Use PdfFileInfo to inspect the file
                using (PdfFileInfo info = new PdfFileInfo(pdfPath))
                {
                    // Encryption status
                    bool isEncrypted = info.IsEncrypted;

                    // Privilege configuration (if any)
                    DocumentPrivilege privilege = info.GetDocumentPrivilege();

                    // Attempt to retrieve the encryption algorithm.
                    // PdfFileInfo does not expose the algorithm directly, so we report "N/A"
                    // when the document is not encrypted or the algorithm cannot be determined.
                    string algorithm = "N/A";

                    // If the file is encrypted, try to open it without a password to see if
                    // the algorithm can be inferred via an exception (placeholder logic).
                    if (isEncrypted)
                    {
                        try
                        {
                            // Attempt to open without password – will throw if encrypted.
                            using (Document doc = new Document(pdfPath))
                            {
                                // If opened, the file is not actually encrypted (unlikely here).
                                algorithm = "None";
                            }
                        }
                        catch (InvalidPasswordException)
                        {
                            // The file is encrypted; algorithm information is not exposed via PdfFileInfo.
                            // In a real scenario, you might use a different API to retrieve it.
                            algorithm = "Unknown";
                        }
                    }

                    // Write entry to the summary
                    writer.WriteLine($"File: {Path.GetFileName(pdfPath)}");
                    writer.WriteLine($"  Encrypted: {(isEncrypted ? "Yes" : "No")}");
                    writer.WriteLine($"  Algorithm: {algorithm}");
                    writer.WriteLine($"  Privileges: {privilege}");
                    writer.WriteLine();
                }
            }
        }

        Console.WriteLine($"Encryption summary written to '{summaryPath}'.");
    }
}