using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileSecurity, DocumentPrivilege, KeySize, Algorithm

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPath = "reencrypted_output.pdf";

        // Existing passwords for the encrypted PDF
        const string userPassword = "user123";
        // Old owner password is not required for re‑encryption; kept for reference only
        // const string oldOwnerPassword = "oldOwner123";

        // New owner password to be set
        const string newOwnerPassword = "newOwner456";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Bind the encrypted PDF (no password needed for BindPdf overload)
            var security = new PdfFileSecurity();
            security.BindPdf(inputPath);

            // Re‑encrypt the PDF with the same user password, a new owner password,
            // 256‑bit AES encryption.
            security.EncryptFile(
                userPassword,
                newOwnerPassword,
                DocumentPrivilege.ForbidAll, // adjust privileges as needed
                KeySize.x256,
                Algorithm.AES);

            // Save the newly encrypted PDF
            security.Save(outputPath);

            Console.WriteLine($"PDF re‑encrypted and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
