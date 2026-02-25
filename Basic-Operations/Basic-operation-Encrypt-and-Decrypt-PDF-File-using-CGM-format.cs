using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input CGM file and intermediate/output paths
        const string cgmPath        = "input.cgm";
        const string pdfPath        = "output.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";

        // Passwords for encryption/decryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        // -------------------------------------------------
        // 1. Load CGM (input‑only format) and save as PDF
        // -------------------------------------------------
        CgmLoadOptions loadOptions = new CgmLoadOptions(); // default A4 page size
        using (Document doc = new Document(cgmPath, loadOptions))
        {
            // Save the converted PDF
            doc.Save(pdfPath);
        }

        // -------------------------------------------------
        // 2. Encrypt the PDF using AES‑256
        // -------------------------------------------------
        using (Document doc = new Document(pdfPath))
        {
            // Define permissions (example: allow printing and content extraction)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt with user/owner passwords and AES‑256 algorithm
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(encryptedPath);
        }

        // -------------------------------------------------
        // 3. Decrypt the encrypted PDF
        // -------------------------------------------------
        using (Document encDoc = new Document(encryptedPath, userPassword))
        {
            // Decrypt (no parameters needed)
            encDoc.Decrypt();

            // Save the decrypted PDF
            encDoc.Save(decryptedPath);
        }

        Console.WriteLine("Encryption and decryption completed successfully.");
    }
}