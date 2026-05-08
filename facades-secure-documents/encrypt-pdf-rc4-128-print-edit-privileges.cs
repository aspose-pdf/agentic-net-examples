using System;
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

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfFileSecurity facade to apply security settings
        using (PdfFileSecurity fileSecurity = new PdfFileSecurity())
        {
            // Bind the existing PDF document
            fileSecurity.BindPdf(inputPath);

            // Create a privilege that allows printing
            DocumentPrivilege privilege = DocumentPrivilege.Print;

            // Enable editing (modify contents) on top of printing
            privilege.AllowModifyContents = true;

            // Encrypt the PDF using RC4‑128 (KeySize.x128 with Algorithm.RC4)
            fileSecurity.EncryptFile(userPassword, ownerPassword, privilege, KeySize.x128, Algorithm.RC4);

            // Save the encrypted PDF to the output path
            fileSecurity.Save(outputPath);
        }

        Console.WriteLine($"PDF encrypted and saved to '{outputPath}'.");
    }
}