using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.Pdf;               // Aspose.Pdf core API
using Aspose.Pdf.Security;    // CryptoAlgorithm and Permissions enums are in Aspose.Pdf namespace (no separate Security namespace needed, but this using is harmless)

class BatchEncryptAndArchive
{
    // Generates a random password using a GUID (ensures uniqueness)
    private static string GeneratePassword()
    {
        return Guid.NewGuid().ToString("N"); // 32‑character alphanumeric string
    }

    static void Main()
    {
        // Input folder containing PDFs to process
        const string inputFolder = @"C:\InputPdfs";
        // Temporary folder to store encrypted PDFs before archiving
        const string tempFolder = @"C:\TempEncrypted";
        // Output ZIP archive path
        const string zipPath = @"C:\EncryptedPdfs.zip";

        // Ensure temporary folder exists and is empty
        if (Directory.Exists(tempFolder))
            Directory.Delete(tempFolder, true);
        Directory.CreateDirectory(tempFolder);

        // Dictionary to keep track of original file name -> password (optional, for reference)
        var passwordMap = new Dictionary<string, string>();

        // Process each PDF file in the input folder
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfFile);
            string encryptedFilePath = Path.Combine(tempFolder, fileName);
            string userPassword = GeneratePassword();
            string ownerPassword = GeneratePassword(); // could be same or different; using separate for demonstration

            // Store passwords (optional)
            passwordMap[fileName] = userPassword;

            // Load, encrypt, and save the PDF using Aspose.Pdf
            using (Document doc = new Document(pdfFile))
            {
                // Define permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt with AES‑256 algorithm
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save encrypted PDF (PDF format, no SaveOptions needed)
                doc.Save(encryptedFilePath);
            }
        }

        // Create ZIP archive containing all encrypted PDFs
        if (File.Exists(zipPath))
            File.Delete(zipPath);

        ZipFile.CreateFromDirectory(tempFolder, zipPath, CompressionLevel.Optimal, false);

        // Optional: write passwords to a text file for reference
        string passwordFile = Path.Combine(Path.GetDirectoryName(zipPath), "Passwords.txt");
        using (StreamWriter writer = new StreamWriter(passwordFile))
        {
            foreach (var kvp in passwordMap)
            {
                writer.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }

        // Cleanup temporary folder
        Directory.Delete(tempFolder, true);

        Console.WriteLine($"Encryption complete. Archive created at: {zipPath}");
        Console.WriteLine($"Passwords saved to: {passwordFile}");
    }
}