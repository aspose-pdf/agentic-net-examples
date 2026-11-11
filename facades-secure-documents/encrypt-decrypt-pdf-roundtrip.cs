using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections; // Added for StructuralComparisons
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace included as requested

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
        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Permissions to allow printing and content extraction
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt with AES‑256 (preferred algorithm)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save encrypted PDF
                doc.Save(encryptedPath);
            }
            Console.WriteLine($"Encrypted PDF saved to '{encryptedPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Encryption failed: {ex.Message}");
            return;
        }

        // ---------- Decrypt ----------
        try
        {
            // Open encrypted PDF with the user password
            using (Document encDoc = new Document(encryptedPath, userPassword))
            {
                // Decrypt (no parameters)
                encDoc.Decrypt();

                // Save the decrypted copy
                encDoc.Save(decryptedPath);
            }
            Console.WriteLine($"Decrypted PDF saved to '{decryptedPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Decryption failed: {ex.Message}");
            return;
        }

        // ---------- Verify round‑trip integrity ----------
        try
        {
            byte[] originalHash  = ComputeSha256(inputPath);
            byte[] decryptedHash = ComputeSha256(decryptedPath);

            bool identical = StructuralComparisons.StructuralEqualityComparer.Equals(originalHash, decryptedHash);
            Console.WriteLine(identical
                ? "Round‑trip verification succeeded: original and decrypted files are identical."
                : "Round‑trip verification failed: files differ.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Verification failed: {ex.Message}");
        }
    }

    // Helper to compute SHA‑256 hash of a file
    static byte[] ComputeSha256(string filePath)
    {
        using (FileStream stream = File.OpenRead(filePath))
        using (SHA256 sha = SHA256.Create())
        {
            return sha.ComputeHash(stream);
        }
    }
}
