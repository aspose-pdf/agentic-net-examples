using System;
using Aspose.Pdf.Facades;   // Facade classes for PDF security operations
using Aspose.Pdf;           // Enums used by the facade (DocumentPrivilege, KeySize)

class Program
{
    static void Main()
    {
        // Input PDF to be encrypted
        const string inputPath  = "input.pdf";
        // Output encrypted PDF
        const string outputPath = "encrypted.pdf";

        // User password (required) – the password a reader must supply to open the PDF
        const string userPassword  = "user123";
        // Owner password – can be null or empty; a random value will be generated if omitted
        const string ownerPassword = "owner123";

        // Define the access privileges for the encrypted document.
        // Here we allow only printing; adjust as needed (e.g., DocumentPrivilege.None).
        DocumentPrivilege privilege = DocumentPrivilege.Print;

        // Choose the key size for encryption – 256‑bit AES is the strongest supported option.
        KeySize keySize = KeySize.x256;

        // Ensure the source file exists before proceeding.
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use the PdfFileSecurity facade to encrypt the PDF.
        // The constructor takes the source and destination file paths.
        using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath))
        {
            // EncryptFile returns true on success; it throws on failure as well.
            bool success = fileSecurity.EncryptFile(userPassword, ownerPassword, privilege, keySize);
            if (!success)
            {
                Console.Error.WriteLine("Encryption failed (method returned false).");
                return;
            }
        }

        Console.WriteLine($"PDF encrypted successfully: {outputPath}");
    }
}