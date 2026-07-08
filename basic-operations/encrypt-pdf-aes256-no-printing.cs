using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Encrypt the PDF with AES‑256 and no permissions (printing is disabled)
        using (Document doc = new Document(inputPath))
        {
            Permissions noPermissions = (Permissions)0; // no flags set
            doc.Encrypt(userPassword, ownerPassword, noPermissions, CryptoAlgorithm.AESx256);
            doc.Save(encryptedPath);
        }

        // Verify that the document is encrypted
        using (Document encryptedDoc = new Document(encryptedPath, userPassword))
        {
            Console.WriteLine($"IsEncrypted: {encryptedDoc.IsEncrypted}");
            // Decrypt to ensure the password works (no parameters needed)
            encryptedDoc.Decrypt();
        }

        Console.WriteLine("Encryption completed and verified.");
    }
}