using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ---------- Encrypt ----------
        // PdfFileSecurity facade works with file paths directly.
        // The constructor (inputFile, outputFile) sets source and destination.
        using (PdfFileSecurity encryptor = new PdfFileSecurity(inputPath, encryptedPath))
        {
            // Encrypt with user and owner passwords, allow printing, 256‑bit AES.
            bool encryptSuccess = encryptor.EncryptFile(
                userPassword: "user123",
                ownerPassword: "owner123",
                privilege: DocumentPrivilege.Print,
                keySize: KeySize.x256);

            if (!encryptSuccess)
            {
                Console.Error.WriteLine("Encryption failed.");
                return;
            }
        }

        // Optional: check that the output file is indeed encrypted
        PdfFileInfo encryptedInfo = new PdfFileInfo(encryptedPath);
        Console.WriteLine($"Encrypted file IsEncrypted: {encryptedInfo.IsEncrypted}");

        // ---------- Decrypt ----------
        using (PdfFileSecurity decryptor = new PdfFileSecurity(encryptedPath, decryptedPath))
        {
            // Decrypt using the owner password set during encryption.
            bool decryptSuccess = decryptor.DecryptFile("owner123");
            if (!decryptSuccess)
            {
                Console.Error.WriteLine("Decryption failed.");
                return;
            }
        }

        // ---------- Verify round‑trip ----------
        long originalSize = new FileInfo(inputPath).Length;
        long decryptedSize = new FileInfo(decryptedPath).Length;

        Console.WriteLine($"Original size:  {originalSize} bytes");
        Console.WriteLine($"Decrypted size: {decryptedSize} bytes");
        Console.WriteLine($"Round‑trip integrity: {(originalSize == decryptedSize ? "OK" : "Mismatch")}");
    }
}