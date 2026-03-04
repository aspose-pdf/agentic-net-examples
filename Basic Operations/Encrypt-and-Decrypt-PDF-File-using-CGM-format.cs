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
            Console.Error.WriteLine($"Input file not found: {cgmInputPath}");
            return;
        }

        // 1. Load CGM and convert to PDF
        using (Document pdfDoc = new Document(cgmInputPath, new CgmLoadOptions()))
        {
            // 2. Encrypt the PDF
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            pdfDoc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // 3. Save the encrypted PDF
            pdfDoc.Save(encryptedPath);
        }

        // 4. Open the encrypted PDF with the user password, decrypt, and save the plain PDF
        using (Document encryptedDoc = new Document(encryptedPath, userPassword))
        {
            encryptedDoc.Decrypt();               // Decrypt in‑memory
            encryptedDoc.Save(decryptedPath);     // Save the decrypted version
        }

        Console.WriteLine("Encryption and decryption completed successfully.");
    }
}