using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input directory containing PDFs to encrypt
        const string inputDir = @"C:\PdfInput";
        // Output directory where encrypted PDFs will be saved
        const string outputDir = @"C:\PdfEncrypted";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // User password to apply to all PDFs
        const string userPassword = "UserPassword123";
        // Owner password (can be same as user password or different)
        const string ownerPassword = "OwnerPassword123";

        // Permissions to allow (example: allow printing and content extraction)
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Iterate over all .pdf files in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            // Determine output file name (same name, placed in output directory)
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string encryptedPath = Path.Combine(outputDir, $"{fileName}_encrypted.pdf");

            try
            {
                // Load the source PDF and encrypt it inside a using block
                using (Document doc = new Document(pdfPath))
                {
                    // Apply encryption using AES-256 (recommended)
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                    // Save the encrypted PDF to the target location
                    doc.Save(encryptedPath);
                }

                Console.WriteLine($"Encrypted: {pdfPath} → {encryptedPath}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch encryption completed.");
    }
}