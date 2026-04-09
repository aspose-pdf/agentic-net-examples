using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing source PDFs
        const string inputDir = "input_pdfs";
        // Directory where encrypted PDFs will be saved
        const string outputDir = "encrypted_pdfs";
        // User password to apply to all PDFs
        const string userPassword = "user123";
        // Owner password (optional, can be empty)
        const string ownerPassword = "";

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
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputDir, fileName);

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(inputPath))
                {
                    // Define desired permissions (example: allow printing and content extraction)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt using AES‑256 algorithm
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                    // Save the encrypted document to the output folder
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Encrypted: {fileName} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt '{fileName}': {ex.Message}");
            }
        }
    }
}