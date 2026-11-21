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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ---------- Encrypt the PDF ----------
        // No permissions granted (including printing)
        using (Document doc = new Document(inputPath))
        {
            Permissions perms = (Permissions)0; // no permissions
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
            doc.Save(encryptedPath);
        }

        // ---------- Verify security settings ----------
        // Open the encrypted file with the user password.
        using (Document encDoc = new Document(encryptedPath, userPassword))
        {
            // Check that the document is indeed encrypted.
            Console.WriteLine($"IsEncrypted: {encDoc.IsEncrypted}");

            // Cast the integer Permissions value to the enum before using HasFlag.
            Permissions currentPerms = (Permissions)encDoc.Permissions;
            bool canPrint = currentPerms.HasFlag(Permissions.PrintDocument);
            Console.WriteLine($"Printing permission granted: {canPrint}");

            // List all granted permissions (should be none).
            Console.WriteLine($"Granted permissions: {currentPerms}");
        }
    }
}
