using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "secured_formfill.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF, apply security, and save.
        using (Document doc = new Document(inputPath))
        {
            // Allow only form filling. All other actions (printing, editing, extracting) are denied.
            Permissions allowedPermissions = Permissions.FillForm;

            // Use the strongest symmetric algorithm (AES‑256).
            doc.Encrypt(userPassword, ownerPassword, allowedPermissions, CryptoAlgorithm.AESx256);

            // Save the encrypted document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'. Only form filling is permitted.");
    }
}