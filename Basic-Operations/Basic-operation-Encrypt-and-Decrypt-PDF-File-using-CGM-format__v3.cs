using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security; // CryptoAlgorithm and Permissions are in Aspose.Pdf namespace; this using is optional but safe

class Program
{
    static void Main()
    {
        const string cgmInputPath   = "input.cgm";          // source CGM file
        const string encryptedPath  = "encrypted.pdf";      // encrypted PDF output
        const string decryptedPath  = "decrypted.pdf";      // decrypted PDF output
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        // Verify the CGM source exists
        if (!File.Exists(cgmInputPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmInputPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Load CGM and convert to PDF
            // ------------------------------------------------------------
            // CgmLoadOptions is the correct way to load a CGM file (input‑only format)
            CgmLoadOptions loadOptions = new CgmLoadOptions();

            using (Document pdfDoc = new Document(cgmInputPath, loadOptions))
            {
                // ------------------------------------------------------------
                // 2. Encrypt the PDF
                // ------------------------------------------------------------
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                pdfDoc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                pdfDoc.Save(encryptedPath);
            }

            // ------------------------------------------------------------
            // 3. Open the encrypted PDF, decrypt it, and save the plain version
            // ------------------------------------------------------------
            using (Document encryptedDoc = new Document(encryptedPath, userPassword))
            {
                // Decrypt() takes no parameters; the password was supplied when opening
                encryptedDoc.Decrypt();

                // Save the decrypted (plain) PDF
                encryptedDoc.Save(decryptedPath);
            }

            Console.WriteLine("CGM → PDF conversion, encryption, and decryption completed successfully.");
            Console.WriteLine($"Encrypted PDF : {encryptedPath}");
            Console.WriteLine($"Decrypted PDF : {decryptedPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}