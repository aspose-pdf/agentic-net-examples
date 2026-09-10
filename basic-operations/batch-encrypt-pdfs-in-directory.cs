using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (contains Document, Permissions, CryptoAlgorithm)

class BatchEncryptPdf
{
    static void Main()
    {
        // Input directory containing PDFs to encrypt
        const string inputDir = @"C:\Pdf\Input";
        // Output directory where encrypted PDFs will be saved
        const string outputDir = @"C:\Pdf\Encrypted";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Define the password to be used for all files
        const string userPassword = "UserPassword123";
        const string ownerPassword = "OwnerPassword123";

        // Define permissions (adjust as needed)
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Iterate over all PDF files in the input directory
        foreach (string inputPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            try
            {
                // Build the output file path (preserve original file name)
                string fileName = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputDir, $"{fileName}_encrypted.pdf");

                // Load the source PDF
                using (Document doc = new Document(inputPath))
                {
                    // Apply encryption using AES-256
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                    // Save the encrypted PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Encrypted: {Path.GetFileName(inputPath)} → {Path.GetFileName(outputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt '{inputPath}': {ex.Message}");
            }
        }
    }
}
