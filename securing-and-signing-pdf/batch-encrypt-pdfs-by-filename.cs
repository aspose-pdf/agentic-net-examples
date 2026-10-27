using System;
using System.IO;
using Aspose.Pdf;

class BatchEncryptPdf
{
    static void Main()
    {
        // Folder containing PDFs to encrypt
        const string inputFolder = @"C:\PdfFolder";

        // Verify folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Get all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to encrypt.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Derive a password from the file name (without extension)
                string fileName = Path.GetFileNameWithoutExtension(pdfPath);
                string userPassword = fileName;          // user password
                string ownerPassword = "ownerPassword"; // constant owner password

                // Permissions to allow printing and content extraction (adjust as needed)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Load, encrypt, and save the PDF (overwrites the original file)
                using (Document doc = new Document(pdfPath))
                {
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                    doc.Save(pdfPath); // Save back to the same path
                }

                Console.WriteLine($"Encrypted: {Path.GetFileName(pdfPath)} (password = \"{userPassword}\")");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch encryption completed.");
    }
}