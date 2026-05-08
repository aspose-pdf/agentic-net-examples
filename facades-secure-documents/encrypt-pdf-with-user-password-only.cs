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

        // User password to protect the PDF; owner password is left undefined (null)
        const string userPassword = "user123";
        string ownerPassword = null; // null or string.Empty will cause a random owner password to be generated

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the PdfFileSecurity facade with input and output files
            PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

            // Encrypt the PDF using a user password only.
            // DocumentPrivilege.Print grants printing rights; adjust as needed.
            // KeySize.x256 provides strong 256‑bit encryption.
            bool encrypted = fileSecurity.EncryptFile(
                userPassword,
                ownerPassword,
                DocumentPrivilege.Print,
                KeySize.x256);

            if (encrypted)
                Console.WriteLine($"PDF encrypted successfully. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Encryption failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during encryption: {ex.Message}");
        }
    }
}