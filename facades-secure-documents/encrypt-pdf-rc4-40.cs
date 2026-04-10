using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = ""; // optional owner password

        // Verify that the source PDF exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileSecurity implements IDisposable, so wrap it in a using block
        using (PdfFileSecurity security = new PdfFileSecurity())
        {
            // Bind the source PDF
            security.BindPdf(inputPath);

            // Encrypt using RC4‑40 (KeySize.x40) and RC4 algorithm.
            // DocumentPrivilege.Print allows printing; adjust as needed.
            bool encrypted = security.EncryptFile(
                userPassword,
                ownerPassword,
                DocumentPrivilege.Print,
                KeySize.x40,
                Algorithm.RC4);

            if (!encrypted)
            {
                Console.Error.WriteLine("Failed to encrypt the PDF.");
                return;
            }

            // Save the encrypted PDF to the desired output path
            security.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}
