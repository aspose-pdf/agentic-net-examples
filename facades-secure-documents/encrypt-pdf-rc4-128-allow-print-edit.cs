using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // ---------------------------------------------------------------------
        // 1️⃣ Create a minimal PDF in‑memory so the sandbox has a file to work on.
        // ---------------------------------------------------------------------
        using (Document seed = new Document())
        {
            seed.Pages.Add(); // at least one blank page
            seed.Save(inputPath);
        }

        // ---------------------------------------------------------------------
        // 2️⃣ Combine privilege settings – allow printing and editing (modify contents).
        // ---------------------------------------------------------------------
        DocumentPrivilege privilege = DocumentPrivilege.ForbidAll;
        privilege.AllowPrint = true;
        privilege.AllowModifyContents = true;

        // ---------------------------------------------------------------------
        // 3️⃣ Encrypt the PDF with RC4‑128 using the non‑obsolete PdfFileSecurity API.
        // ---------------------------------------------------------------------
        PdfFileSecurity fileSecurity = new PdfFileSecurity();
        // Bind the source PDF first.
        fileSecurity.BindPdf(inputPath);
        // Encrypt with the desired passwords, privileges, key size and algorithm.
        fileSecurity.EncryptFile(
            userPassword,
            ownerPassword,
            privilege,
            KeySize.x128,
            Algorithm.RC4);
        // Save the encrypted PDF to the output path.
        fileSecurity.Save(outputPath);

        Console.WriteLine($"Encryption succeeded. Output saved to '{outputPath}'.");
    }
}
