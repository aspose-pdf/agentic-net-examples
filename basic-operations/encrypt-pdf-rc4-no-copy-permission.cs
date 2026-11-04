using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted.pdf";
        const string decryptedPath  = "decrypted.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Encrypt the PDF using RC4 (128‑bit) and allow only printing (no copying)
        using (Document doc = new Document(inputPath))
        {
            Permissions perms = Permissions.PrintDocument; // no ExtractContent permission
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.RC4x128);
            doc.Save(encryptedPath);
        }

        // Verify the security settings of the encrypted document
        using (Document encDoc = new Document(encryptedPath, userPassword))
        {
            Console.WriteLine($"IsEncrypted: {encDoc.IsEncrypted}");

            // Document.Permissions returns an int; cast it to the Permissions enum before using HasFlag
            Permissions perms = (Permissions)encDoc.Permissions;
            Console.WriteLine($"Permissions: {perms}");

            bool copyingAllowed = perms.HasFlag(Permissions.ExtractContent);
            Console.WriteLine($"Copying allowed: {copyingAllowed}");
        }

        // Optional: decrypt the PDF and save the clear version
        using (Document encDoc = new Document(encryptedPath, userPassword))
        {
            encDoc.Decrypt(); // Decrypt with no parameters
            encDoc.Save(decryptedPath);
        }

        Console.WriteLine("Encryption and verification completed.");
    }
}
