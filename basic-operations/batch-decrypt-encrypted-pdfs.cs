using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing encrypted PDFs
        const string inputFolder = "EncryptedPdfs";
        // Folder where decrypted PDFs will be saved
        const string outputFolder = "DecryptedPdfs";
        // Shared owner password for all PDFs
        const string ownerPassword = "owner123";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] encryptedFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string encryptedPath in encryptedFiles)
        {
            // Build output file name (optional suffix to avoid overwriting)
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(encryptedPath);
            string decryptedPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}_decrypted.pdf");

            try
            {
                // Open the encrypted PDF using the owner password
                using (Document doc = new Document(encryptedPath, ownerPassword))
                {
                    // Remove encryption; Decrypt() takes no parameters
                    doc.Decrypt();

                    // Save the unprotected copy as a regular PDF
                    doc.Save(decryptedPath);
                }

                Console.WriteLine($"Decrypted: {Path.GetFileName(encryptedPath)} → {decryptedPath}");
            }
            catch (Exception ex)
            {
                // Log any errors (e.g., wrong password, corrupted file)
                Console.Error.WriteLine($"Failed to decrypt '{encryptedPath}': {ex.Message}");
            }
        }
    }
}