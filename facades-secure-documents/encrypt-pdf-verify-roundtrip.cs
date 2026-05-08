using System;
using System.IO;
using Aspose.Pdf;               // Document, DocumentPrivilege
using Aspose.Pdf.Facades;      // PdfFileSecurity, KeySize

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted.pdf";
        const string decryptedPath  = "decrypted.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        // Ensure the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ---------- Encrypt the PDF ----------
        // PdfFileSecurity implements IDisposable, so wrap in using
        using (PdfFileSecurity encryptor = new PdfFileSecurity())
        {
            // Bind the source PDF file
            encryptor.BindPdf(inputPath);

            // Encrypt with user/owner passwords, allow printing, 256‑bit AES
            encryptor.EncryptFile(userPassword, ownerPassword,
                                  DocumentPrivilege.Print, KeySize.x256);

            // Save the encrypted PDF
            encryptor.Save(encryptedPath);
        }

        // ---------- Decrypt the PDF ----------
        using (PdfFileSecurity decryptor = new PdfFileSecurity())
        {
            // Bind the encrypted PDF file
            decryptor.BindPdf(encryptedPath);

            // Decrypt using the owner password (or user password if no owner)
            decryptor.DecryptFile(ownerPassword);

            // Save the decrypted PDF
            decryptor.Save(decryptedPath);
        }

        // ---------- Verify round‑trip integrity ----------
        // Load both original and decrypted PDFs and compare page counts
        using (Document original   = new Document(inputPath))
        using (Document roundTrip = new Document(decryptedPath))
        {
            bool pageCountMatches = original.Pages.Count == roundTrip.Pages.Count;
            Console.WriteLine($"Page count match: {pageCountMatches}");
        }

        Console.WriteLine("Encryption and decryption completed successfully.");
    }
}