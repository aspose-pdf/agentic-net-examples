using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf; // Aspose.Pdf contains Document, Permissions, CryptoAlgorithm

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "input_pdfs";
        // Folder where encrypted PDFs will be written
        const string outputFolder = "encrypted_pdfs";
        // Path of the resulting ZIP archive
        const string zipPath = "encrypted_archive.zip";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify that the input directory exists; if not, create it and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. Creating it now. Please place PDF files in this folder and rerun the program.");
            Directory.CreateDirectory(inputFolder);
            return; // Nothing to process yet
        }

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'. Nothing to encrypt.");
            return;
        }

        // Create (or overwrite) the ZIP archive
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update))
        {
            // Process each PDF file in the input folder
            foreach (string pdfFilePath in pdfFiles)
            {
                string fileName = Path.GetFileName(pdfFilePath);
                string encryptedFilePath = Path.Combine(outputFolder, fileName);

                // Generate unique passwords for this document
                string userPassword = Guid.NewGuid().ToString("N");
                string ownerPassword = Guid.NewGuid().ToString("N");

                // Load, encrypt, and save the PDF using Aspose.Pdf
                using (Document doc = new Document(pdfFilePath))
                {
                    // Define permissions (example: allow printing and content extraction)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt with AES‑256 (preferred algorithm)
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                    // Save the encrypted PDF
                    doc.Save(encryptedFilePath);
                }

                // Add the encrypted PDF to the ZIP archive
                archive.CreateEntryFromFile(encryptedFilePath, fileName);
            }
        }

        Console.WriteLine($"All PDFs encrypted and archived to '{zipPath}'.");
    }
}
