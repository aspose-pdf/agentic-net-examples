using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

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

        // ---------- Encrypt ----------
        using (PdfFileSecurity encryptor = new PdfFileSecurity())
        {
            // Bind the source PDF
            encryptor.BindPdf(inputPath);

            // Encrypt with user/owner passwords, allow printing, 256‑bit AES
            bool encryptSuccess = encryptor.EncryptFile(
                userPassword,
                ownerPassword,
                DocumentPrivilege.Print,
                KeySize.x256);

            if (!encryptSuccess)
                throw new InvalidOperationException("Encryption failed.");

            // Save the encrypted PDF
            encryptor.Save(encryptedPath);
        }

        // ---------- Decrypt ----------
        using (PdfFileSecurity decryptor = new PdfFileSecurity())
        {
            // Bind the encrypted PDF
            decryptor.BindPdf(encryptedPath);

            // Decrypt using the owner password
            bool decryptSuccess = decryptor.DecryptFile(ownerPassword);

            if (!decryptSuccess)
                throw new InvalidOperationException("Decryption failed.");

            // Save the decrypted PDF
            decryptor.Save(decryptedPath);
        }

        // ---------- Verify round‑trip integrity ----------
        using (Document original = new Document(inputPath))
        using (Document roundTrip = new Document(decryptedPath))
        {
            // Compare page count
            if (original.Pages.Count != roundTrip.Pages.Count)
            {
                Console.WriteLine("Integrity check failed: page count differs.");
                return;
            }

            // Extract text from both documents
            TextAbsorber origAbsorber = new TextAbsorber();
            original.Pages.Accept(origAbsorber);

            TextAbsorber roundAbsorber = new TextAbsorber();
            roundTrip.Pages.Accept(roundAbsorber);

            // Compare extracted text
            if (origAbsorber.Text == roundAbsorber.Text)
                Console.WriteLine("Integrity check passed: content matches.");
            else
                Console.WriteLine("Integrity check failed: content differs.");
        }
    }
}