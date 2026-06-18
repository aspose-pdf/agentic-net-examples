using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the encrypted output PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";

        // Passwords for encryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Ensure the source file exists before proceeding
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the PdfFileSecurity facade with input and output files
            PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

            // Encrypt using RC4‑40 (KeySize.x40 with Algorithm.RC4) and set a basic privilege
            // DocumentPrivilege.Print allows printing; adjust as needed.
            fileSecurity.EncryptFile(
                userPassword,
                ownerPassword,
                DocumentPrivilege.Print,
                KeySize.x40,
                Algorithm.RC4);

            // Release any resources held by the facade
            fileSecurity.Close();

            Console.WriteLine($"PDF encrypted successfully and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Encryption failed: {ex.Message}");
        }
    }
}