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
        // Output ZIP file that will hold encrypted PDFs
        const string outputZipPath = "encrypted_pdfs.zip";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Create (or overwrite) the ZIP archive
        using (FileStream zipStream = new FileStream(outputZipPath, FileMode.Create))
        using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Update))
        {
            // Process each PDF file in the input folder
            foreach (string pdfFilePath in Directory.GetFiles(inputFolder, "*.pdf"))
            {
                string fileName = Path.GetFileName(pdfFilePath);
                // Generate a unique password for this PDF (e.g., a GUID without hyphens)
                string password = Guid.NewGuid().ToString("N");

                // Load the PDF document
                using (Document doc = new Document(pdfFilePath))
                {
                    // Define permissions (example: allow printing and content extraction)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                    // Encrypt using user and owner passwords and AES-256 algorithm
                    doc.Encrypt(password, password, perms, CryptoAlgorithm.AESx256);

                    // Save the encrypted PDF into a memory stream
                    using (MemoryStream encryptedStream = new MemoryStream())
                    {
                        doc.Save(encryptedStream);
                        encryptedStream.Position = 0; // Reset stream position for reading

                        // Add the encrypted PDF as an entry in the ZIP archive
                        ZipArchiveEntry entry = zip.CreateEntry(fileName, CompressionLevel.Optimal);
                        using (Stream entryStream = entry.Open())
                        {
                            encryptedStream.CopyTo(entryStream);
                        }
                    }
                }

                Console.WriteLine($"Encrypted '{fileName}' with password: {password}");
            }
        }

        Console.WriteLine($"All PDFs have been encrypted and archived to '{outputZipPath}'.");
    }
}