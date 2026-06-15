using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, Permissions, CryptoAlgorithm

class BatchEncryptAndArchive
{
    static void Main()
    {
        // Input folder containing PDF files
        const string inputFolder = @"C:\InputPdfs";
        // Output ZIP file path
        const string outputZipPath = @"C:\EncryptedPdfs.zip";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Collect PDF file paths
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        // Dictionary to keep track of generated passwords (optional, for logging)
        var passwordMap = new Dictionary<string, string>();

        // Create the ZIP archive
        using (FileStream zipStream = new FileStream(outputZipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
        {
            foreach (string pdfPath in pdfFiles)
            {
                // Generate a unique password for this PDF
                string password = Guid.NewGuid().ToString("N");
                passwordMap[Path.GetFileName(pdfPath)] = password;

                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Define permissions (example: allow printing and content extraction)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt using user and owner passwords (both set to the generated password)
                    doc.Encrypt(password, password, perms, CryptoAlgorithm.AESx256);

                    // Save encrypted PDF into a memory stream
                    using (MemoryStream encryptedStream = new MemoryStream())
                    {
                        doc.Save(encryptedStream);
                        encryptedStream.Position = 0; // Reset stream position for reading

                        // Create a new entry in the ZIP archive with the same file name
                        ZipArchiveEntry entry = archive.CreateEntry(Path.GetFileName(pdfPath), CompressionLevel.Optimal);
                        using (Stream entryStream = entry.Open())
                        {
                            encryptedStream.CopyTo(entryStream);
                        }
                    }
                }

                Console.WriteLine($"Encrypted and added to ZIP: {Path.GetFileName(pdfPath)}");
            }
        }

        // Optional: output passwords for reference
        Console.WriteLine("\nGenerated passwords:");
        foreach (var kvp in passwordMap)
        {
            Console.WriteLine($"{kvp.Key} => {kvp.Value}");
        }

        Console.WriteLine($"\nAll PDFs have been encrypted and archived to: {outputZipPath}");
    }
}