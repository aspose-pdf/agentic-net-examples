using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing source PDFs
        const string inputDir = "InputPdfs";
        // Directory where encrypted PDFs will be written
        const string outputDir = "EncryptedPdfs";

        // Same user and owner passwords for all files
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Ensure input directory exists
        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Create output directory if it does not exist
        Directory.CreateDirectory(outputDir);

        // Define desired permissions (example: allow printing and content extraction)
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Process each PDF file in the input directory
        foreach (string sourcePath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(sourcePath);
            string encryptedPath = Path.Combine(outputDir, $"{fileNameWithoutExt}_enc.pdf");

            try
            {
                // Load the source PDF inside a using block for deterministic disposal
                using (Document doc = new Document(sourcePath))
                {
                    // Encrypt using user/owner passwords, permissions, and AES‑256 algorithm
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                    // Save the encrypted PDF
                    doc.Save(encryptedPath);
                }

                Console.WriteLine($"Encrypted: {encryptedPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt '{sourcePath}': {ex.Message}");
            }
        }
    }
}