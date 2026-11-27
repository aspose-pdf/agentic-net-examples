using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Facades;      // Included as requested (not used directly)

class Program
{
    static void Main()
    {
        // Paths
        const string inputPath  = "encrypted_input.pdf";
        const string outputPath = "encrypted_output.pdf";

        // Current passwords (used to open the encrypted PDF)
        const string oldUserPassword  = "oldUserPwd";
        const string oldOwnerPassword = "oldOwnerPwd"; // not required for opening, kept for reference

        // New passwords to set
        const string newUserPassword  = "newUserPwd";
        const string newOwnerPassword = "newOwnerPwd";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the encrypted PDF using the current user password
        using (Document doc = new Document(inputPath, oldUserPassword))
        {
            // Define permissions – allow printing
            Permissions perms = Permissions.PrintDocument;

            // Re‑encrypt the document with new passwords, desired permissions,
            // and AES‑256 encryption (CryptoAlgorithm.AESx256 corresponds to KeySize.x256)
            doc.Encrypt(newUserPassword, newOwnerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF re‑encrypted and saved to '{outputPath}'.");
    }
}