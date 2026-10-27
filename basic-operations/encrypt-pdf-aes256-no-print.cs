using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the original PDF and apply AES‑256 encryption with no permissions (no printing)
            using (Document doc = new Document(inputPath))
            {
                // No permissions – use a zero‑valued Permissions flag
                Permissions perms = (Permissions)0;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                doc.Save(encryptedPath);
            }

            // Open the encrypted PDF using the user password and verify security settings
            using (Document encDoc = new Document(encryptedPath, userPassword))
            {
                // Verify that the document is indeed encrypted
                bool isEncrypted = encDoc.IsEncrypted;

                // Retrieve the permissions that were set during encryption (cast from int)
                Permissions currentPerms = (Permissions)encDoc.Permissions;

                // Determine whether printing is allowed (should be false)
                bool canPrint = (currentPerms & Permissions.PrintDocument) == Permissions.PrintDocument;

                Console.WriteLine($"Encrypted: {isEncrypted}");
                Console.WriteLine($"Printing allowed: {canPrint}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
