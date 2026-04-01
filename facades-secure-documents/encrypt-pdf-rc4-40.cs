using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (PdfFileSecurity fileSecurity = new PdfFileSecurity())
            {
                fileSecurity.BindPdf(inputPath);
                // Encrypt with RC4‑40 algorithm, allowing printing privilege
                fileSecurity.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x40, Algorithm.RC4);
                fileSecurity.Save(outputPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
