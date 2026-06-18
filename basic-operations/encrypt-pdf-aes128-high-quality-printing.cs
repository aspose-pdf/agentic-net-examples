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

        // Encrypt the PDF with AES‑128 and allow high‑quality printing
        using (Document doc = new Document(inputPath))
        {
            Permissions perms = Permissions.PrintDocument | Permissions.PrintingQuality;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx128);
            doc.Save(encryptedPath);
        }

        // Verify encryption by opening with the user password and decrypting
        using (Document encryptedDoc = new Document(encryptedPath, userPassword))
        {
            encryptedDoc.Decrypt();               // remove encryption
            encryptedDoc.Save(decryptedPath);     // save decrypted copy to confirm success
            Console.WriteLine($"Verification succeeded, decrypted file saved to '{decryptedPath}'.");
        }

        Console.WriteLine("PDF encryption with AES‑128 and high‑quality printing permission completed.");
    }
}