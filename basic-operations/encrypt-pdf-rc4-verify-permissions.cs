using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted_rc4.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ---------- Encrypt ----------
        // Permissions: allow printing only (no ExtractContent => no copying)
        Permissions permissions = Permissions.PrintDocument;

        using (Document doc = new Document(inputPath))
        {
            // Use RC4 128‑bit algorithm
            doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.RC4x128);
            doc.Save(encryptedPath);
        }
        Console.WriteLine($"Encrypted PDF saved to '{encryptedPath}'.");

        // ---------- Verify encryption ----------
        // 1. Opening without a password should fail
        bool encryptionDetected = false;
        try
        {
            using (Document _ = new Document(encryptedPath))
            {
                // If we get here, the file was not encrypted (unexpected)
            }
        }
        catch (PdfException)
        {
            encryptionDetected = true; // Expected exception for encrypted file
        }

        Console.WriteLine(encryptionDetected
            ? "Encryption verified: file requires a password."
            : "Encryption verification failed: file opened without a password.");

        // 2. Opening with the user password should succeed
        try
        {
            using (Document doc = new Document(encryptedPath, userPassword))
            {
                // Simple operation to prove the document is readable
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                Console.WriteLine($"Document opened with user password. Extracted text length: {absorber.Text.Length}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to open encrypted PDF with user password: {ex.Message}");
        }
    }
}