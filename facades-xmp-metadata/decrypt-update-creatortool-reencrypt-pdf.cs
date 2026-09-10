using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // -----------------------------------------------------------------
        // 0. Prepare a sample PDF and encrypt it (so the sandbox has a file).
        // -----------------------------------------------------------------
        const string originalPath   = "original.pdf";      // plain PDF we create
        const string protectedPath  = "protected.pdf";     // password‑protected source PDF
        const string decryptedPath = "decrypted_temp.pdf"; // temporary decrypted copy
        const string outputPath    = "reprotected.pdf";    // final encrypted PDF

        const string ownerPassword   = "owner123"; // original owner password
        const string newUserPassword = "newUser"; // new user password after re‑encrypt
        const string newOwnerPassword= "newOwner"; // new owner password after re‑encrypt

        // ---------------------------------------------------------------
        // 0.1 Create a simple PDF that we will later protect.
        // ---------------------------------------------------------------
        using (Document seed = new Document())
        {
            seed.Pages.Add(); // add a blank page
            seed.Save(originalPath);
        }

        // ---------------------------------------------------------------
        // 0.2 Encrypt the seed PDF with the original owner password.
        // ---------------------------------------------------------------
        using (PdfFileSecurity encryptor = new PdfFileSecurity())
        {
            encryptor.BindPdf(originalPath);
            // Empty user password, owner password = ownerPassword, allow printing.
            encryptor.EncryptFile(
                "",                     // userPassword
                ownerPassword,          // ownerPassword
                DocumentPrivilege.Print,
                KeySize.x256,
                Algorithm.AES);
            encryptor.Save(protectedPath);
        }

        // ---------------------------------------------------------------
        // 1. Decrypt the PDF using the original owner password.
        // ---------------------------------------------------------------
        using (PdfFileSecurity decryptor = new PdfFileSecurity())
        {
            decryptor.BindPdf(protectedPath);
            // Decrypt and write the clear‑text PDF to a temporary file.
            decryptor.DecryptFile(ownerPassword);
            decryptor.Save(decryptedPath);
        }

        // ---------------------------------------------------------------
        // 2. Update the Creator metadata (CreatorTool).
        // ---------------------------------------------------------------
        using (PdfFileInfo info = new PdfFileInfo(decryptedPath))
        {
            info.Creator = "MyApp CreatorTool v1.0";
            info.SaveNewInfo(decryptedPath);
        }

        // ---------------------------------------------------------------
        // 3. Re‑encrypt the PDF with new passwords and privileges.
        // ---------------------------------------------------------------
        using (PdfFileSecurity reEncryptor = new PdfFileSecurity())
        {
            reEncryptor.BindPdf(decryptedPath);
            reEncryptor.EncryptFile(
                newUserPassword,
                newOwnerPassword,
                DocumentPrivilege.Print,
                KeySize.x256,
                Algorithm.AES);
            reEncryptor.Save(outputPath);
        }

        // Clean up the temporary decrypted file.
        try { File.Delete(decryptedPath); } catch { }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
