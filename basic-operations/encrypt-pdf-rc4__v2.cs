using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted_rc4.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Encrypt the PDF with RC4 and no copying permission (no permissions granted)
        using (Document doc = new Document(inputPath))
        {
            // (Permissions)0 represents "no permissions" – all actions (including copy) are denied
            Permissions noPermissions = (Permissions)0;
            doc.Encrypt(userPassword, ownerPassword, noPermissions, CryptoAlgorithm.RC4x128);
            doc.Save(encryptedPath);
        }

        // Verify the security settings by opening with the user password
        using (Document encryptedDoc = new Document(encryptedPath, userPassword))
        {
            Console.WriteLine($"Is Encrypted: {encryptedDoc.IsEncrypted}");
            Console.WriteLine($"Effective Permissions: {encryptedDoc.Permissions}");
        }
    }
}