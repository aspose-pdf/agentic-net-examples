using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the original PDF and encrypt it with AES‑256, no permissions (printing disabled)
        using (Document doc = new Document(inputPath))
        {
            // No permissions – use a zero‑valued enum cast
            Permissions perms = (Permissions)0; // equivalent to "no permissions"
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
            doc.Save(encryptedPath);
        }

        // Verify that the encrypted document does not allow printing
        using (Document encDoc = new Document(encryptedPath, userPassword))
        {
            // Decrypt to make permissions accessible (optional, Decrypt has no parameters)
            encDoc.Decrypt();
            // Permissions property returns an int, so cast to the enum before checking flags
            bool canPrint = ((Permissions)encDoc.Permissions).HasFlag(Permissions.PrintDocument);
            Console.WriteLine($"Print permission enabled: {canPrint}");
        }
    }
}
