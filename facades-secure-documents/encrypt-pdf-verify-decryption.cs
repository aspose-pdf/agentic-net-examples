using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // ---------- Encrypt ----------
        // PdfFileSecurity works with file paths: (input, output)
        using (PdfFileSecurity encryptor = new PdfFileSecurity(inputPath, encryptedPath))
        {
            // Encrypt with Print privilege and 256‑bit AES key
            bool encResult = encryptor.EncryptFile(userPassword, ownerPassword,
                                                   DocumentPrivilege.Print, KeySize.x256);
            if (!encResult)
            {
                Console.Error.WriteLine("Encryption failed.");
                return;
            }
        }

        // ---------- Decrypt ----------
        using (PdfFileSecurity decryptor = new PdfFileSecurity(encryptedPath, decryptedPath))
        {
            // Decrypt using the owner password (or user password if no owner password)
            bool decResult = decryptor.DecryptFile(ownerPassword);
            if (!decResult)
            {
                Console.Error.WriteLine("Decryption failed.");
                return;
            }
        }

        // ---------- Verify round‑trip integrity ----------
        // Compute SHA‑256 hash of original and decrypted files
        string originalHash = ComputeSha256(inputPath);
        string decryptedHash = ComputeSha256(decryptedPath);

        Console.WriteLine($"Original SHA‑256 : {originalHash}");
        Console.WriteLine($"Decrypted SHA‑256: {decryptedHash}");

        if (originalHash.Equals(decryptedHash, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Success: Decrypted file matches the original.");
        }
        else
        {
            Console.WriteLine("Failure: Decrypted file differs from the original.");
        }
    }

    // Helper method to compute SHA‑256 hash of a file
    private static string ComputeSha256(string filePath)
    {
        using (FileStream stream = File.OpenRead(filePath))
        using (SHA256 sha = SHA256.Create())
        {
            byte[] hash = sha.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }
    }
}