using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "protected.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the PdfFileSecurity facade
            PdfFileSecurity security = new PdfFileSecurity();

            // Bind the source PDF file
            security.BindPdf(inputPath);

            // Use a privilege that forbids all operations (no printing, editing, etc.)
            DocumentPrivilege privilege = DocumentPrivilege.ForbidAll;

            // Encrypt with strong AES‑256 encryption and the specified passwords
            security.EncryptFile(userPassword, ownerPassword, privilege, KeySize.x256, Algorithm.AES);

            // Save the encrypted PDF to the output path
            security.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine($"Protected PDF saved to '{outputPath}'.");
    }
}