using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the original PDFs
        const string inputFolder = "InputPdfs";
        // Folder where encrypted PDFs will be written
        const string outputFolder = "EncryptedPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            // Derive password from the file name (without extension)
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string userPassword = baseName;
            string ownerPassword = baseName;

            // Destination path for the encrypted PDF
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

            try
            {
                // Load the PDF, encrypt it, and save the result
                using (Document doc = new Document(pdfPath))
                {
                    // Define desired permissions (example: allow printing and content extraction)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt using AES‑256 (preferred algorithm)
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                    // Save the encrypted PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Encrypted: {pdfPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt '{pdfPath}': {ex.Message}");
            }
        }
    }
}