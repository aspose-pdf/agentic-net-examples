using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security; // Permissions enum lives here

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted_rc4.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Encrypt the PDF with RC4 (128‑bit) and no permissions (copying disabled)
        using (Document doc = new Document(inputPath))
        {
            // (Permissions)0 means "no permissions granted"
            Permissions perms = (Permissions)0;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.RC4x128);
            doc.Save(encryptedPath);
        }

        // Open the encrypted PDF using the user password and verify the security settings
        using (Document encDoc = new Document(encryptedPath, userPassword))
        {
            // Verify that the document is indeed encrypted
            bool isEncrypted = encDoc.IsEncrypted;
            Console.WriteLine($"IsEncrypted: {isEncrypted}");

            // Permissions property may return an int in some library versions – cast to Permissions
            Permissions currentPerms = (Permissions)encDoc.Permissions;
            Console.WriteLine($"Permissions: {currentPerms}");

            // Additional check: ensure that ExtractContent permission is not set
            bool canExtract = (currentPerms & Permissions.ExtractContent) == Permissions.ExtractContent;
            Console.WriteLine($"Copying allowed (ExtractContent): {canExtract}");
        }
    }
}
