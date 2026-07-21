using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source PDFs
        const string inputDir = "input_pdfs";
        // Directory where encrypted PDFs will be saved
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

        // Retrieve all PDF files in the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            // Build the output file name (e.g., MyDoc_encrypted.pdf)
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDir, $"{fileName}_encrypted.pdf");

            try
            {
                // Load the source PDF
                using (Document doc = new Document(inputPath))
                {
                    // Define desired permissions (allow printing and content extraction)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt using AES‑256 algorithm
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                    // Save the encrypted PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Encrypted: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt '{inputPath}': {ex.Message}");
            }
        }
    }
}