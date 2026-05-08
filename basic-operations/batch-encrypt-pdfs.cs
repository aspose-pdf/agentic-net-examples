using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing source PDFs
        const string inputDirectory = "input_pdfs";
        // Directory where encrypted PDFs will be saved
        const string outputDirectory = "encrypted_pdfs";

        // Passwords to apply to every PDF
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Verify input directory exists
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files in the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            // Build output file name (original name with suffix)
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string encryptedPath = Path.Combine(outputDirectory, $"{fileNameWithoutExt}_encrypted.pdf");

            try
            {
                // Load the PDF, encrypt it, and save the encrypted copy
                using (Document doc = new Document(pdfPath))
                {
                    // Define permissions (adjust as needed)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt using AES‑256 (preferred algorithm)
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                    // Save the encrypted PDF
                    doc.Save(encryptedPath);
                }

                Console.WriteLine($"Encrypted: {encryptedPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error encrypting '{pdfPath}': {ex.Message}");
            }
        }
    }
}