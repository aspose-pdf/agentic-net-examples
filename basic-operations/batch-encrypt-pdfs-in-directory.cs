using System;
using System.IO;
using Aspose.Pdf;

class BatchEncryptPdf
{
    static void Main()
    {
        // Input directory containing PDFs to encrypt
        const string inputDir = @"C:\PdfInput";
        // Output directory where encrypted PDFs will be saved
        const string outputDir = @"C:\PdfEncrypted";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Passwords for encryption
        const string userPassword = "UserPassword123";
        const string ownerPassword = "OwnerPassword123";

        // Permissions to grant on the encrypted PDFs
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Process each PDF file in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            // Build output file name (original name + "_encrypted.pdf")
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string encryptedPath = Path.Combine(outputDir, $"{fileName}_encrypted.pdf");

            try
            {
                // Load the source PDF, encrypt it, and save the encrypted copy
                using (Document doc = new Document(pdfPath))
                {
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                    doc.Save(encryptedPath);
                }

                Console.WriteLine($"Encrypted: {Path.GetFileName(pdfPath)} → {Path.GetFileName(encryptedPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt '{pdfPath}': {ex.Message}");
            }
        }
    }
}