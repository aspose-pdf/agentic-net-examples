using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to inspect
        const string pdfFolder = @"C:\PdfCollection";

        if (!Directory.Exists(pdfFolder))
        {
            Console.Error.WriteLine($"Folder not found: {pdfFolder}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(pdfFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        Console.WriteLine("Encryption Algorithm Report");
        Console.WriteLine(new string('=', 30));

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load PDF metadata using PdfFileInfo (facade)
                using (PdfFileInfo info = new PdfFileInfo(pdfPath))
                {
                    bool isEncrypted = info.IsEncrypted;
                    string algorithmName = "None";

                    if (isEncrypted)
                    {
                        // Retrieve privilege settings; DocumentPrivilege contains encryption details
                        var privilege = info.GetDocumentPrivilege();

                        // The DocumentPrivilege class exposes the CryptoAlgorithm used for encryption.
                        // If the property name differs, adjust accordingly.
                        // Example: privilege.CryptoAlgorithm or privilege.Algorithm
                        // Here we use reflection as a fallback to obtain the enum name.
                        var algoProp = privilege.GetType().GetProperty("CryptoAlgorithm");
                        if (algoProp != null)
                        {
                            var algoValue = algoProp.GetValue(privilege);
                            algorithmName = algoValue?.ToString() ?? "Unknown";
                        }
                        else
                        {
                            // Fallback: try a property named "Algorithm"
                            algoProp = privilege.GetType().GetProperty("Algorithm");
                            if (algoProp != null)
                            {
                                var algoValue = algoProp.GetValue(privilege);
                                algorithmName = algoValue?.ToString() ?? "Unknown";
                            }
                        }
                    }

                    Console.WriteLine($"File: {Path.GetFileName(pdfPath)}");
                    Console.WriteLine($"  Encrypted: {isEncrypted}");
                    Console.WriteLine($"  Algorithm: {algorithmName}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Report generation completed.");
    }
}