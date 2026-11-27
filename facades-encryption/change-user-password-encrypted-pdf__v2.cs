using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace is available, though not required for this task

class Program
{
    static void Main()
    {
        const string inputPath      = "encrypted_input.pdf";
        const string outputPath     = "reencrypted_output.pdf";
        const string oldUserPassword = "oldPass";
        const string newUserPassword = "newPass";
        const string ownerPassword   = "ownerPass";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Verify whether the PDF is encrypted
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
        Console.WriteLine($"IsEncrypted: {fileInfo.IsEncrypted}");

        // Load the encrypted PDF using the current user password
        using (Document doc = new Document(inputPath, oldUserPassword))
        {
            // Define desired permissions (adjust as needed)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Re‑encrypt the document with a new user password
            // CryptoAlgorithm enum is in Aspose.Pdf namespace
            doc.Encrypt(newUserPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"User password changed and PDF saved to '{outputPath}'.");
    }
}