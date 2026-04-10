using System;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // needed for creating a placeholder PDF if it does not exist

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the encrypted output PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";

        // Passwords for the encrypted PDF
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // ---------------------------------------------------------------------
        // Ensure the source PDF exists. If it is missing (as in the original
        // example) we create a minimal PDF so the sample runs without a
        // FileNotFoundException. In production you would supply a real file.
        // ---------------------------------------------------------------------
        if (!System.IO.File.Exists(inputPath))
        {
            var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPath);
        }

        // Use the non‑obsolete PdfFileSecurity constructor, bind the source PDF,
        // apply 128‑bit AES encryption with the desired privileges, and finally
        // save the encrypted document.
        using (PdfFileSecurity fileSecurity = new PdfFileSecurity())
        {
            fileSecurity.BindPdf(inputPath);

            // Define privileges: forbid all operations (no printing, no editing, etc.)
            DocumentPrivilege privilege = DocumentPrivilege.ForbidAll;

            // Encrypt using 128‑bit AES (KeySize.x128) and the AES algorithm
            fileSecurity.EncryptFile(userPassword, ownerPassword, privilege, KeySize.x128, Algorithm.AES);

            // Persist the encrypted PDF to the output path
            fileSecurity.Save(outputPath);
        }

        Console.WriteLine($"PDF encrypted and saved to '{outputPath}'.");
    }
}
