using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted_rc4.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Record original file size
        long originalSize = new FileInfo(inputPath).Length;

        try
        {
            // Load the PDF and apply 128‑bit RC4 encryption
            using (Document doc = new Document(inputPath))
            {
                // Example permissions: allow printing and content extraction
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt with user password, owner password, permissions, and RC4x128 algorithm
                doc.Encrypt(userPassword: "user123", ownerPassword: "owner123", perms, CryptoAlgorithm.RC4x128);
                doc.Save(encryptedPath);
            }

            // Record encrypted file size
            long encryptedSize = new FileInfo(encryptedPath).Length;

            Console.WriteLine($"Original size:   {originalSize} bytes");
            Console.WriteLine($"Encrypted size:  {encryptedSize} bytes");

            // Verify that the encrypted file is slightly larger
            if (encryptedSize > originalSize)
                Console.WriteLine("File size increased after encryption (as expected).");
            else
                Console.WriteLine("File size did not increase; encryption may not have been applied correctly.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}