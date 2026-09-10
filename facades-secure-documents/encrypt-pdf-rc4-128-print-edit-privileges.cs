using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";

        // Passwords for encryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // ---------------------------------------------------------------------
        // Ensure a source PDF exists – the sandbox does not contain external files.
        // Create a minimal one‑page PDF if it is missing.
        // ---------------------------------------------------------------------
        if (!System.IO.File.Exists(inputPath))
        {
            var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPath);
        }

        // Combine privileges: allow printing and allow editing (modify contents)
        DocumentPrivilege privilege = DocumentPrivilege.Print;   // start with printing allowed
        privilege.AllowModifyContents = true;                    // enable editing

        // ---------------------------------------------------------------------
        // Use the non‑obsolete PdfFileSecurity API:
        //   1. Bind the source PDF.
        //   2. Encrypt with the desired algorithm/key size.
        //   3. Save the encrypted result to a new file.
        // ---------------------------------------------------------------------
        PdfFileSecurity fileSecurity = new PdfFileSecurity();
        fileSecurity.BindPdf(inputPath);
        fileSecurity.EncryptFile(userPassword, ownerPassword, privilege, KeySize.x128, Algorithm.RC4);
        fileSecurity.Save(outputPath);
        fileSecurity.Close();

        Console.WriteLine($"PDF encrypted and saved to '{outputPath}'.");
    }
}
