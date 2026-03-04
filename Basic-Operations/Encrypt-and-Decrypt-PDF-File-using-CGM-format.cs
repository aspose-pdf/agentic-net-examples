using System;
using System.IO;
using Aspose.Pdf; // Document, CgmLoadOptions, Permissions, CryptoAlgorithm

class Program
{
    static void Main()
    {
        const string cgmInputPath   = "input.cgm";      // CGM source file
        const string encryptedPath  = "encrypted.pdf";  // Encrypted PDF output
        const string decryptedPath  = "decrypted.pdf";  // Decrypted PDF output
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        // Verify input file exists
        if (!File.Exists(cgmInputPath))
        {
            Console.Error.WriteLine($"Input CGM file not found: {cgmInputPath}");
            return;
        }

        try
        {
            // -------------------------------------------------
            // 1. Load CGM and convert to PDF (Document is PDF)
            // -------------------------------------------------
            CgmLoadOptions loadOptions = new CgmLoadOptions(); // default A4 page size
            using (Document pdfDoc = new Document(cgmInputPath, loadOptions))
            {
                // -------------------------------------------------
                // 2. Encrypt the PDF with user/owner passwords
                // -------------------------------------------------
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                pdfDoc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                pdfDoc.Save(encryptedPath);
            }

            // -------------------------------------------------
            // 3. Open the encrypted PDF, decrypt it, and save plain PDF
            // -------------------------------------------------
            using (Document encryptedDoc = new Document(encryptedPath, userPassword))
            {
                // Decrypt (no parameters)
                encryptedDoc.Decrypt();

                // Save the decrypted (plain) PDF
                encryptedDoc.Save(decryptedPath);
            }

            Console.WriteLine("Encryption and decryption completed successfully.");
            Console.WriteLine($"Encrypted PDF: {encryptedPath}");
            Console.WriteLine($"Decrypted PDF: {decryptedPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}