using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputFolder = "input_pdfs";
        const string tempFolder = "encrypted_pdfs";
        const string zipPath = "encrypted_archive.zip";

        // Verify that the input folder exists before trying to enumerate files.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. No PDFs to process.");
            return;
        }

        // Clean/create temporary folder for encrypted PDFs.
        if (Directory.Exists(tempFolder))
            Directory.Delete(tempFolder, true);
        Directory.CreateDirectory(tempFolder);

        // Get all PDF files in the input folder.
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the input folder.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            // Generate unique passwords for each PDF.
            string userPassword = Guid.NewGuid().ToString("N");
            string ownerPassword = Guid.NewGuid().ToString("N");

            // Load, encrypt, and save the PDF.
            using (Document doc = new Document(pdfPath))
            {
                // Allow printing and content extraction.
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt with AES‑256 (core API, no Facades).
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                string encryptedFileName = Path.GetFileNameWithoutExtension(pdfPath) + "_enc.pdf";
                string encryptedPath = Path.Combine(tempFolder, encryptedFileName);
                doc.Save(encryptedPath);
            }
        }

        // Create a ZIP archive containing all encrypted PDFs.
        if (File.Exists(zipPath))
            File.Delete(zipPath);
        ZipFile.CreateFromDirectory(tempFolder, zipPath);
        Console.WriteLine($"Encrypted archive created at '{zipPath}'.");
    }
}
