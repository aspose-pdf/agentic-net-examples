using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the original and encrypted PDF files
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted_rc4_128.pdf";

        // Passwords for encryption
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Record the original file size
        long originalSize = new FileInfo(inputPath).Length;

        // Encrypt the PDF using 128‑bit RC4 (CryptoAlgorithm.RC4x128)
        using (Document doc = new Document(inputPath))
        {
            // Define permissions (example: allow printing and content extraction)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Apply encryption
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.RC4x128);

            // Save the encrypted document
            doc.Save(encryptedPath);
        }

        // Record the encrypted file size
        long encryptedSize = new FileInfo(encryptedPath).Length;

        // Output size information and verification result
        Console.WriteLine($"Original size:  {originalSize} bytes");
        Console.WriteLine($"Encrypted size: {encryptedSize} bytes");

        if (encryptedSize > originalSize)
        {
            Console.WriteLine("File size increased after encryption (as expected).");
        }
        else
        {
            Console.WriteLine("File size did not increase; unexpected.");
        }
    }
}