using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the original PDFs
        const string inputFolder = "InputPdfs";
        // Folder where encrypted PDFs will be written
        const string outputFolder = "EncryptedPdfs";
        // Path to the log file that stores file‑name → password mappings
        const string logFilePath = "passwords.log";

        // Ensure the input and output directories exist
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Open the log file once and keep it open for the whole batch
        using (StreamWriter logWriter = new StreamWriter(logFilePath, append: true))
        {
            // Process every PDF file in the input folder
            foreach (string sourcePath in Directory.GetFiles(inputFolder, "*.pdf"))
            {
                // Derive a deterministic password from the file name (without extension)
                string baseName = Path.GetFileNameWithoutExtension(sourcePath);
                string password = GeneratePassword(baseName);

                // Destination path keeps the original file name
                string destPath = Path.Combine(outputFolder, Path.GetFileName(sourcePath));

                // Load the PDF, encrypt it, and save the encrypted version
                Document doc = new Document(sourcePath);
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                // Encrypt using the same value for user and owner passwords. AES‑256 is the recommended algorithm.
                doc.Encrypt(password, password, perms, CryptoAlgorithm.AESx256);
                doc.Save(destPath);

                // Record the password in the log (plain‑text for demo purposes; replace with a secure store in production)
                logWriter.WriteLine($"{baseName}: {password}");
            }
        }

        Console.WriteLine("Batch encryption completed.");
    }

    // Simple deterministic password generator – replace with a stronger scheme as needed
    static string GeneratePassword(string baseName)
    {
        // Example: reverse the base name and append a constant suffix
        char[] chars = baseName.ToCharArray();
        Array.Reverse(chars);
        return new string(chars) + "_Secure123";
    }
}
