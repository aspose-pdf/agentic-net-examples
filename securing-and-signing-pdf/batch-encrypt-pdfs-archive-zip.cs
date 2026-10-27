using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class BatchEncryptAndArchive
{
    static void Main()
    {
        // Base directory of the application (works for both Windows and Linux)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input / output folders relative to the base directory
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        string encryptedFolder = Path.Combine(baseDir, "EncryptedPdfs");
        string zipPath = Path.Combine(baseDir, "EncryptedArchive.zip");

        // Ensure the folders exist – if they do not, create them so the program does not crash
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(encryptedFolder);

        // Get all PDF files in the input folder. If none are found, inform the user and exit gracefully.
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'. Place PDFs there and rerun the program.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Generate a unique password for this document
                string password = Guid.NewGuid().ToString("N");

                // Determine the output path for the encrypted PDF
                string encryptedPath = Path.Combine(
                    encryptedFolder,
                    Path.GetFileNameWithoutExtension(pdfPath) + "_enc.pdf");

                // Load, encrypt, and save the PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Define permissions (example: allow printing and content extraction)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt using AES‑256 (CryptoAlgorithm enum is in Aspose.Pdf namespace)
                    doc.Encrypt(password, password, perms, CryptoAlgorithm.AESx256);

                    // Save the encrypted PDF
                    doc.Save(encryptedPath);
                }

                // Optional: log the password mapping (commented out to avoid plain‑text storage)
                // File.AppendAllText(Path.Combine(baseDir, "passwords.txt"), $"{Path.GetFileName(pdfPath)}:{password}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(pdfPath)}': {ex.Message}");
            }
        }

        // Create a ZIP archive containing all encrypted PDFs (only if any were created)
        string[] encryptedFiles = Directory.GetFiles(encryptedFolder, "*.pdf");
        if (encryptedFiles.Length > 0)
        {
            try
            {
                using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
                using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update))
                {
                    foreach (string encryptedFile in encryptedFiles)
                    {
                        archive.CreateEntryFromFile(encryptedFile, Path.GetFileName(encryptedFile));
                    }
                }
                Console.WriteLine($"Archive created at '{zipPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create ZIP archive: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("No encrypted PDFs were generated; ZIP archive will not be created.");
        }

        Console.WriteLine("Batch encryption and archiving completed.");
    }
}
