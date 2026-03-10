using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string cgmPath        = "input.cgm";
        const string encryptedPath  = "encrypted.pdf";
        const string decryptedPath  = "decrypted.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        // Load CGM, convert to PDF, then encrypt
        using (Document doc = new Document(cgmPath, new CgmLoadOptions()))
        {
            // Set desired permissions (example: allow printing and content extraction)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt with AES-256
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save encrypted PDF
            doc.Save(encryptedPath);
        }

        // Open encrypted PDF, decrypt, and save the plain PDF
        using (Document encryptedDoc = new Document(encryptedPath, userPassword))
        {
            // Decrypt (no parameters)
            encryptedDoc.Decrypt();

            // Save decrypted PDF
            encryptedDoc.Save(decryptedPath);
        }

        Console.WriteLine($"Encryption and decryption completed.\nEncrypted: {encryptedPath}\nDecrypted: {decryptedPath}");
    }
}