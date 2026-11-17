using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

// Suppress known NuGet vulnerability warnings for System.Security.Cryptography.Pkcs
#pragma warning disable NU1902 // Package has a known moderate severity vulnerability
#pragma warning disable NU1903 // Package has a known high severity vulnerability

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Resolve input folder – fall back to the current directory if missing
        // ---------------------------------------------------------------------
        const string defaultInputFolder = "InputPdfs";
        string inputFolder = Path.GetFullPath(defaultInputFolder);
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' not found. Using current directory instead.");
            inputFolder = Directory.GetCurrentDirectory();
        }

        // ---------------------------------------------------------------------
        // 2. Create a temporary folder for the encrypted PDFs
        // ---------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(),
                                         "EncryptedPdfs_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // ---------------------------------------------------------------------
        // 3. Path for the final ZIP archive (created in the executable folder)
        // ---------------------------------------------------------------------
        string zipPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EncryptedArchive.zip");

        // ---------------------------------------------------------------------
        // 4. Process each PDF in the input folder
        // ---------------------------------------------------------------------
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            if (!File.Exists(pdfPath))
            {
                Console.WriteLine($"Skipping missing file: {pdfPath}");
                continue;
            }

            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string encryptedPath = Path.Combine(tempFolder, fileName + "_enc.pdf");

            // Generate a unique password for this PDF
            string password = Guid.NewGuid().ToString("N");

            try
            {
                // Load, encrypt, and save the PDF using core Aspose.Pdf API
                using (Document doc = new Document(pdfPath))
                {
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                    doc.Encrypt(password, password, perms, CryptoAlgorithm.AESx256);
                    doc.Save(encryptedPath);
                }

                // Record the password for reference (optional)
                string pwdRecord = $"{fileName}_enc.pdf : {password}{Environment.NewLine}";
                File.AppendAllText(Path.Combine(tempFolder, "passwords.txt"), pwdRecord);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        // ---------------------------------------------------------------------
        // 5. Create (or replace) the ZIP archive containing encrypted PDFs + password list
        // ---------------------------------------------------------------------
        if (File.Exists(zipPath))
            File.Delete(zipPath);

        using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
        {
            foreach (string file in Directory.GetFiles(tempFolder, "*.pdf"))
            {
                archive.CreateEntryFromFile(file, Path.GetFileName(file));
            }

            string pwdFile = Path.Combine(tempFolder, "passwords.txt");
            if (File.Exists(pwdFile))
                archive.CreateEntryFromFile(pwdFile, "passwords.txt");
        }

        // ---------------------------------------------------------------------
        // 6. Clean up temporary files
        // ---------------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to delete temporary folder '{tempFolder}': {ex.Message}");
        }
    }
}
#pragma warning restore NU1902
#pragma warning restore NU1903