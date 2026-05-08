using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDF files to analyze
        const string inputFolder = @"C:\PdfFiles";
        // Path for the generated summary CSV file
        const string summaryPath = @"C:\PdfFiles\encryption_summary.csv";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Prepare the CSV header
        using (StreamWriter writer = new StreamWriter(summaryPath, false))
        {
            writer.WriteLine("FileName,IsEncrypted,EncryptionAlgorithm,Privileges");
        }

        // Process each PDF file in the folder
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load PDF meta‑information using the Facades API
                using (PdfFileInfo fileInfo = new PdfFileInfo(pdfFile))
                {
                    bool isEncrypted = fileInfo.IsEncrypted;
                    // Retrieve privilege settings (e.g., Print, Modify, etc.)
                    DocumentPrivilege privileges = fileInfo.GetDocumentPrivilege();

                    // Aspose.Pdf.Facades does not expose the encryption algorithm directly.
                    // If the file is encrypted we report "Unknown", otherwise "None".
                    string algorithm = isEncrypted ? "Unknown" : "None";

                    // Convert the privilege enum to a readable string
                    string privilegeText = privileges.ToString();

                    // Append the information to the CSV summary
                    using (StreamWriter writer = new StreamWriter(summaryPath, true))
                    {
                        string fileName = Path.GetFileName(pdfFile);
                        writer.WriteLine($"{fileName},{isEncrypted},{algorithm},{privilegeText}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfFile}': {ex.Message}");
            }
        }

        Console.WriteLine($"Encryption summary written to: {summaryPath}");
    }
}