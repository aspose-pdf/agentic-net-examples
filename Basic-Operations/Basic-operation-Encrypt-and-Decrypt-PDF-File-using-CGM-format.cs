using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string cgmInputPath   = "input.cgm";          // CGM source file
        const string encryptedPath  = "encrypted.pdf";      // Encrypted PDF output
        const string decryptedPath  = "decrypted.pdf";      // Decrypted PDF output
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(cgmInputPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmInputPath}");
            return;
        }

        // Load CGM and convert to PDF, then encrypt
        CgmLoadOptions loadOptions = new CgmLoadOptions(); // default A4 page size
        using (Document doc = new Document(cgmInputPath, loadOptions))
        {
            // Set desired permissions (example: allow printing and content extraction)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt with AES-256
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save encrypted PDF
            doc.Save(encryptedPath);
        }

        // Open the encrypted PDF, decrypt, and save the plain PDF
        using (Document encryptedDoc = new Document(encryptedPath, userPassword))
        {
            encryptedDoc.Decrypt();               // Decrypt in‑place
            encryptedDoc.Save(decryptedPath);     // Save the decrypted version
        }

        Console.WriteLine($"Encryption complete: {encryptedPath}");
        Console.WriteLine($"Decryption complete: {decryptedPath}");
    }
}