using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPath = "reencrypted_output.pdf";
        const string oldUserPassword = "oldPass";
        const string newUserPassword = "newPass";
        const string ownerPassword = "ownerPass";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Verify that the PDF is encrypted
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
        if (!fileInfo.IsEncrypted)
        {
            Console.WriteLine("PDF is not encrypted; no password change required.");
            return;
        }

        // Load the encrypted PDF using the existing user password
        using (Document doc = new Document(inputPath, oldUserPassword))
        {
            // Define desired permissions (keep existing or customize)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Re‑encrypt with the new user password (owner password stays the same)
            doc.Encrypt(newUserPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the PDF with the updated password
            doc.Save(outputPath);
        }

        Console.WriteLine($"User password changed and PDF saved to '{outputPath}'.");
    }
}