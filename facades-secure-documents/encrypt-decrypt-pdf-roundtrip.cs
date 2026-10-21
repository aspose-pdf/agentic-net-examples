using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted.pdf";
        const string decryptedPath  = "decrypted.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // ---------- Encrypt the PDF ----------
            // Load the source PDF
            Document doc = new Document(inputPath);
            // Define permissions you want to allow after encryption
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            // Apply AES‑256 encryption
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
            // Save the encrypted file
            doc.Save(encryptedPath);

            // ---------- Decrypt the PDF ----------
            // Load the encrypted PDF providing the user password directly (no PdfLoadOptions needed)
            Document encryptedDoc = new Document(encryptedPath, userPassword);
            // Remove encryption (the document is now in clear text)
            encryptedDoc.Decrypt();
            // Save the decrypted copy
            encryptedDoc.Save(decryptedPath);

            // ---------- Verify round‑trip integrity ----------
            string originalHash  = ComputeSha256(inputPath);
            string decryptedHash = ComputeSha256(decryptedPath);

            Console.WriteLine($"Original SHA‑256 : {originalHash}");
            Console.WriteLine($"Decrypted SHA‑256: {decryptedHash}");

            if (originalHash.Equals(decryptedHash, StringComparison.OrdinalIgnoreCase))
                Console.WriteLine("Round‑trip verification succeeded: files are identical.");
            else
                Console.WriteLine("Round‑trip verification failed: files differ.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper to compute SHA‑256 hash of a file
    static string ComputeSha256(string filePath)
    {
        using (FileStream stream = File.OpenRead(filePath))
        using (SHA256 sha = SHA256.Create())
        {
            byte[] hash = sha.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }
    }
}
