using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Encrypt the PDF and save it
        using (Document doc = new Document(inputPath))
        {
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
            doc.Save(encryptedPath);
        }

        // Open the encrypted PDF, decrypt it, and save the decrypted version
        using (Document encryptedDoc = new Document(encryptedPath, userPassword))
        {
            encryptedDoc.Decrypt(); // No parameters required
            encryptedDoc.Save(decryptedPath);
        }

        Console.WriteLine("Encryption and decryption completed successfully.");
    }
}