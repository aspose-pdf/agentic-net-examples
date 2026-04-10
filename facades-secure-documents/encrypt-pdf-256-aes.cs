using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Encrypt the PDF with 256‑bit AES using the specified passwords.
        // PdfFileSecurity handles both encryption and decryption operations.
        using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
        {
            // DocumentPrivilege.Print allows printing; adjust as needed (e.g., DocumentPrivilege.None).
            security.EncryptFile(
                userPassword,
                ownerPassword,
                DocumentPrivilege.Print,
                KeySize.x256,
                Algorithm.AES);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}
