using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade API for PDF security operations
using Aspose.Pdf;          // Needed for DocumentPrivilege, KeySize enums

class BatchEncrypt
{
    static void Main()
    {
        // Directory containing PDFs to encrypt
        const string sourceDir = @"C:\PdfInput";
        // Directory where encrypted PDFs will be written
        const string targetDir = @"C:\PdfEncrypted";

        // Ensure the output directory exists
        Directory.CreateDirectory(targetDir);

        // Define the passwords to apply to every file
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Enumerate all PDF files in the source directory (non‑recursive)
        foreach (string inputPath in Directory.GetFiles(sourceDir, "*.pdf"))
        {
            // Build the output file path – keep the original file name
            string fileName   = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(targetDir, fileName);

            try
            {
                // PdfFileSecurity has a constructor that takes input and output file names
                using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
                {
                    // Encrypt the file:
                    //   - userPassword / ownerPassword as defined above
                    //   - DocumentPrivilege.Print allows printing; adjust as needed
                    //   - KeySize.x256 selects 256‑bit AES encryption (strongest supported)
                    bool success = security.EncryptFile(
                        userPassword,
                        ownerPassword,
                        DocumentPrivilege.Print,
                        KeySize.x256);

                    if (!success)
                    {
                        Console.Error.WriteLine($"Encryption failed for '{inputPath}'.");
                    }
                }

                Console.WriteLine($"Encrypted: {fileName}");
            }
            catch (Exception ex)
            {
                // Log any unexpected errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch encryption completed.");
    }
}