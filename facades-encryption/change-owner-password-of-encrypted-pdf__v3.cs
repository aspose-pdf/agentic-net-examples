using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // PdfFileInfo resides here

class Program
{
    static void Main()
    {
        const string inputPath        = "encrypted_input.pdf";
        const string outputPath       = "reencrypted_output.pdf";
        const string userPassword     = "user123";
        const string newOwnerPassword = "newOwner456";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Verify that the file is encrypted
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
        Console.WriteLine($"IsEncrypted: {fileInfo.IsEncrypted}");

        // Load the encrypted PDF using the user password
        using (Document doc = new Document(inputPath, userPassword))
        {
            // Example permissions: allow printing and content extraction
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Re‑encrypt with the same user password but a new owner password
            doc.Encrypt(userPassword, newOwnerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}