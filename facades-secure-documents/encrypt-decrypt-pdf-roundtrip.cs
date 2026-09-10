using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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
        // Bind the source PDF, encrypt it, and save the encrypted version.
        PdfFileSecurity encryptor = new PdfFileSecurity();
        encryptor.BindPdf(inputPath);                                   // initialize with source file
        encryptor.EncryptFile(userPassword, ownerPassword,
                              DocumentPrivilege.Print,               // set desired privilege
                              KeySize.x256);                         // 256‑bit AES encryption
        encryptor.Save(encryptedPath);                                 // write encrypted PDF
        encryptor.Close();                                             // release resources

        // ---------- Decrypt ----------
        // Bind the encrypted PDF, decrypt it using the owner password, and save the result.
        PdfFileSecurity decryptor = new PdfFileSecurity();
        decryptor.BindPdf(encryptedPath);
        decryptor.DecryptFile(ownerPassword);
        decryptor.Save(decryptedPath);
        decryptor.Close();

        // ---------- Verify round‑trip ----------
        // Load original and decrypted PDFs and compare page counts as a simple integrity check.
        using (Document originalDoc = new Document(inputPath))
        using (Document decryptedDoc = new Document(decryptedPath))
        {
            bool samePageCount = originalDoc.Pages.Count == decryptedDoc.Pages.Count;
            Console.WriteLine(samePageCount
                ? "Round‑trip successful: page count matches."
                : "Round‑trip failed: page count differs.");
        }
    }
}