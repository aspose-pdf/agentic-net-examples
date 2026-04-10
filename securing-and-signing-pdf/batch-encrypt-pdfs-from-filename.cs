using System;
using System.IO;
using Aspose.Pdf; // CryptoAlgorithm, Permissions, Document

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "input_pdfs";
        // Folder where encrypted PDFs will be written
        const string outputFolder = "encrypted_pdfs";

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
            // Derive passwords from the file name (without extension)
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string userPassword = baseName;   // password for opening the document
            string ownerPassword = baseName;  // password for changing permissions

            // Destination path (same file name, different folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

            try
            {
                // Load the PDF, encrypt it, and save the encrypted copy
                using (Document doc = new Document(pdfPath))
                {
                    // Allow printing and content extraction; adjust as needed
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Use AES‑256 encryption (preferred algorithm)
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                    // Save the encrypted document
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Encrypted: {pdfPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error encrypting '{pdfPath}': {ex.Message}");
            }
        }
    }
}