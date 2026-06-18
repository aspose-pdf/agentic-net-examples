using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Aspose.Pdf;

class BatchPdfEncryptor
{
    // Generates a deterministic password from a file name using SHA‑256.
    private static string GeneratePassword(string fileName)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hash = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(fileName));
            // Take first 12 bytes and encode as Base64 for readability.
            return Convert.ToBase64String(hash).Substring(0, 12);
        }
    }

    // Writes an entry to the log file. The entry is encrypted with DPAPI (CurrentUser scope).
    private static void WriteSecureLog(string logPath, string fileName, string password)
    {
        string entry = $"{fileName}:{password}";
        byte[] plainBytes = System.Text.Encoding.UTF8.GetBytes(entry);
        // Protect the data – only the current Windows user can decrypt it.
        byte[] protectedBytes = ProtectedData.Protect(plainBytes, null, DataProtectionScope.CurrentUser);
        string protectedBase64 = Convert.ToBase64String(protectedBytes);

        // Append the encrypted entry as a single line.
        File.AppendAllLines(logPath, new[] { protectedBase64 });
    }

    static void Main()
    {
        // Folder containing PDFs to encrypt.
        const string inputFolder = @"C:\PdfInput";
        // Folder where encrypted PDFs will be saved.
        const string outputFolder = @"C:\PdfEncrypted";
        // Path to the secure log file.
        const string logFile = @"C:\PdfEncryptionLog\passwords.log";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);
        Directory.CreateDirectory(Path.GetDirectoryName(logFile));

        // Define the permissions you want to allow after encryption.
        Permissions allowedPermissions = Permissions.PrintDocument | Permissions.ExtractContent;

        // Process each PDF file in the input folder.
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Create a password derived from the file name.
            string password = GeneratePassword(fileName);

            try
            {
                // Load, encrypt, and save the document using core Aspose.Pdf APIs.
                using (Document doc = new Document(inputPath))
                {
                    doc.Encrypt(
                        userPassword: password,          // User password
                        ownerPassword: password,         // Owner password (same as user for simplicity)
                        permissions: allowedPermissions,
                        cryptoAlgorithm: CryptoAlgorithm.AESx256);

                    doc.Save(outputPath);
                }

                // Record the password securely.
                WriteSecureLog(logFile, fileName, password);

                Console.WriteLine($"Encrypted '{fileName}' → '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch encryption completed.");
    }
}