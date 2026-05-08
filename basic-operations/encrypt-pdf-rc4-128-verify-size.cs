using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths and passwords
        const string inputPath      = "input.pdf";
        const string encryptedPath = "encrypted_rc4_128.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Get original file size
        long originalSize = new FileInfo(inputPath).Length;

        try
        {
            // Load the PDF, encrypt with 128‑bit RC4, and save
            using (Document doc = new Document(inputPath))
            {
                // Define permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt using RC4 128‑bit algorithm
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.RC4x128);

                // Save the encrypted document
                doc.Save(encryptedPath);
            }

            // Get encrypted file size
            long encryptedSize = new FileInfo(encryptedPath).Length;

            // Verify that the encrypted file is slightly larger than the original
            Console.WriteLine($"Original size : {originalSize} bytes");
            Console.WriteLine($"Encrypted size: {encryptedSize} bytes");

            if (encryptedSize > originalSize)
                Console.WriteLine("Encryption succeeded: file size increased as expected.");
            else
                Console.WriteLine("Warning: encrypted file size is not larger than the original.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}