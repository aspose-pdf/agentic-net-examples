using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted_rc4_128.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Get original file size
        long originalSize = new FileInfo(inputPath).Length;

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Define permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt using 128‑bit RC4
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.RC4x128);

                // Save the encrypted PDF
                doc.Save(encryptedPath);
            }

            // Get encrypted file size
            long encryptedSize = new FileInfo(encryptedPath).Length;

            Console.WriteLine($"Original size : {originalSize} bytes");
            Console.WriteLine($"Encrypted size: {encryptedSize} bytes");

            if (encryptedSize > originalSize)
                Console.WriteLine("File size increased after encryption (as expected).");
            else
                Console.WriteLine("File size did not increase; unexpected result.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}