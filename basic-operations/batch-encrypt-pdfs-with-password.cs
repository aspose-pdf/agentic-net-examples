using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input directory containing PDFs to encrypt
        const string inputDir = "input_pdfs";
        // Output directory where encrypted PDFs will be saved
        const string outputDir = "encrypted_pdfs";

        // User and owner passwords (same for all files)
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Define desired permissions for the encrypted PDFs
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Process each PDF file in the input directory
        foreach (string filePath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string outputPath = Path.Combine(outputDir, $"{fileName}_encrypted.pdf");

            try
            {
                // Load the PDF, encrypt it, and save the encrypted version
                using (Document doc = new Document(filePath))
                {
                    // Encrypt using AES-256 algorithm
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Encrypted: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt '{filePath}': {ex.Message}");
            }
        }
    }
}