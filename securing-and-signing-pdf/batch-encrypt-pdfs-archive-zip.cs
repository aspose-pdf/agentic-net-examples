using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, Permissions, CryptoAlgorithm)

class BatchEncryptAndArchive
{
    static void Main()
    {
        // Input folder containing original PDFs
        const string inputFolder = @"C:\PdfInput";
        // Temporary folder to store encrypted PDFs before archiving
        const string encryptedFolder = @"C:\PdfEncrypted";
        // Output ZIP file path
        const string zipPath = @"C:\PdfArchive\encrypted_pdfs.zip";

        // Ensure the output directories exist
        Directory.CreateDirectory(encryptedFolder);
        Directory.CreateDirectory(Path.GetDirectoryName(zipPath));

        // Collect paths of encrypted PDFs for later zipping
        List<string> encryptedFiles = new List<string>();

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Generate a unique password for this document (using a GUID)
            string userPassword = Guid.NewGuid().ToString();
            string ownerPassword = Guid.NewGuid().ToString();

            // Define desired permissions (example: allow printing and content extraction)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Determine the output path for the encrypted PDF
            string encryptedPath = Path.Combine(encryptedFolder, Path.GetFileName(pdfPath));

            // Load, encrypt, and save the document using proper disposal pattern
            using (Document doc = new Document(pdfPath))
            {
                // Encrypt with AES-256 (preferred algorithm) and the generated passwords
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                // Save the encrypted PDF (PDF format, no SaveOptions needed)
                doc.Save(encryptedPath);
            }

            // Remember the encrypted file for archiving
            encryptedFiles.Add(encryptedPath);
        }

        // Create a ZIP archive containing all encrypted PDFs
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
        {
            foreach (string filePath in encryptedFiles)
            {
                // Add each encrypted PDF to the archive; the entry name is just the file name
                archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
            }
        }

        // Optional: clean up temporary encrypted files
        foreach (string filePath in encryptedFiles)
        {
            try { File.Delete(filePath); } catch { /* ignore cleanup errors */ }
        }

        Console.WriteLine($"Encryption complete. Archive created at: {zipPath}");
    }
}