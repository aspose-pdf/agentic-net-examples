using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths for the input CGM file and the intermediate PDF files
        const string cgmPath        = "input.cgm";
        const string pdfPath        = "converted.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";

        // Passwords for encryption/decryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify the CGM source exists
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Load the CGM file and convert it to a PDF document
            // ------------------------------------------------------------
            var cgmLoadOptions = new CgmLoadOptions(); // default A4 300dpi page size
            using (Document pdfDoc = new Document(cgmPath, cgmLoadOptions))
            {
                // Save the intermediate PDF (optional, just to have a clear file)
                pdfDoc.Save(pdfPath);
            }

            // ------------------------------------------------------------
            // 2. Encrypt the PDF using AES-256
            // ------------------------------------------------------------
            using (Document docToEncrypt = new Document(pdfPath))
            {
                // Define permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt with the recommended CryptoAlgorithm.AESx256
                docToEncrypt.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                docToEncrypt.Save(encryptedPath);
            }

            // ------------------------------------------------------------
            // 3. Decrypt the previously encrypted PDF
            // ------------------------------------------------------------
            // Open the encrypted PDF supplying the user password
            using (Document encryptedDoc = new Document(encryptedPath, userPassword))
            {
                // Decrypt – no parameters required
                encryptedDoc.Decrypt();

                // Save the decrypted PDF
                encryptedDoc.Save(decryptedPath);
            }

            Console.WriteLine("CGM → PDF conversion, encryption, and decryption completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}