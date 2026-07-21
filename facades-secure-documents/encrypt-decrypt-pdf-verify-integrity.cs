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

        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Encrypt the PDF using PdfFileSecurity
        using (PdfFileSecurity encryptor = new PdfFileSecurity(inputPath, encryptedPath))
        {
            // Encrypt with user/owner passwords, allow printing, 256‑bit AES
            bool encryptSuccess = encryptor.EncryptFile(
                userPassword,
                ownerPassword,
                DocumentPrivilege.Print,
                KeySize.x256);

            if (!encryptSuccess)
            {
                Console.Error.WriteLine("Encryption failed.");
                return;
            }
        }

        // Decrypt the previously encrypted PDF
        using (PdfFileSecurity decryptor = new PdfFileSecurity(encryptedPath, decryptedPath))
        {
            // Decrypt using the owner password
            bool decryptSuccess = decryptor.DecryptFile(ownerPassword);

            if (!decryptSuccess)
            {
                Console.Error.WriteLine("Decryption failed.");
                return;
            }
        }

        // Simple integrity check: compare file sizes
        if (File.Exists(decryptedPath))
        {
            long originalSize = new FileInfo(inputPath).Length;
            long decryptedSize = new FileInfo(decryptedPath).Length;

            Console.WriteLine($"Original size:  {originalSize} bytes");
            Console.WriteLine($"Decrypted size: {decryptedSize} bytes");
            Console.WriteLine(originalSize == decryptedSize
                ? "Round‑trip integrity verified."
                : "Round‑trip integrity check failed.");
        }
    }
}