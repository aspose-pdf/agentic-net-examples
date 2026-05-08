using System;
using System.IO;
using Aspose.Pdf;               // DocumentPrivilege, KeySize enums
using Aspose.Pdf.Facades;      // PdfFileSecurity class

class BatchEncryptPdf
{
    static void Main(string[] args)
    {
        // Directory containing PDFs – can be passed as first argument or defaulted.
        string inputDirectory = args.Length > 0 ? args[0] : @"C:\PdfFolder";

        // User and owner passwords to apply to every file.
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Directory not found: {inputDirectory}");
            return;
        }

        // Process each PDF file in the directory.
        foreach (string inputPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            // Build output file name (e.g., MyDoc_encrypted.pdf).
            string outputPath = Path.Combine(
                inputDirectory,
                Path.GetFileNameWithoutExtension(inputPath) + "_encrypted.pdf");

            try
            {
                // PdfFileSecurity implements IDisposable – use a using block.
                using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath))
                {
                    // Encrypt with desired privileges and key size.
                    // DocumentPrivilege.Print allows printing; adjust as needed.
                    // KeySize.x256 provides strong AES‑256 encryption.
                    fileSecurity.EncryptFile(userPassword, ownerPassword,
                                            DocumentPrivilege.Print, KeySize.x256);
                }

                Console.WriteLine($"Encrypted: {inputPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files.
                Console.Error.WriteLine($"Error encrypting '{inputPath}': {ex.Message}");
            }
        }
    }
}