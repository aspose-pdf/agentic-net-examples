using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for security operations
using Aspose.Pdf;           // DocumentPrivilege, KeySize, Algorithm enums

class PdfSecurityExample
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Passwords (can be null or empty if not required)
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ------------------------------------------------------------
        // Example 1: Set access permissions (e.g., allow only printing)
        // ------------------------------------------------------------
        using (PdfFileSecurity security = new PdfFileSecurity(inputPdf, outputPdf))
        {
            // Choose a predefined privilege. Here we allow printing only.
            DocumentPrivilege privilege = DocumentPrivilege.Print;

            // Apply the privilege using the original passwords.
            // This creates a new PDF at outputPdf with the specified permissions.
            security.SetPrivilege(userPassword, ownerPassword, privilege);
        }

        // ------------------------------------------------------------
        // Example 2: Encrypt the PDF with a specific algorithm and key size
        // ------------------------------------------------------------
        const string encryptedPdf = "encrypted.pdf";

        using (PdfFileSecurity security = new PdfFileSecurity(inputPdf, encryptedPdf))
        {
            // Define the privilege you want to apply after encryption.
            // For demonstration we allow printing and copying.
            DocumentPrivilege privilege = DocumentPrivilege.AllowAll;

            // Encrypt using 256‑bit AES. KeySize.x256 + Algorithm.AES is a valid combination.
            security.EncryptFile(userPassword, ownerPassword, privilege, KeySize.x256, Algorithm.AES);
        }

        Console.WriteLine("Security operations completed successfully.");
    }
}