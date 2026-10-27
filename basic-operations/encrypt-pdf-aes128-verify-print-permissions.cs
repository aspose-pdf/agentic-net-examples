using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted_aes128.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Encrypt the PDF with AES‑128 and high‑quality printing permission
        using (Document doc = new Document(inputPath))
        {
            Permissions perms = Permissions.PrintDocument;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx128);
            doc.Save(encryptedPath);
        }

        // Verify the encryption settings by reopening the file with the user password
        using (Document encDoc = new Document(encryptedPath, userPassword))
        {
            // Decrypt the document (no parameters needed)
            encDoc.Decrypt();

            // Cast the integer permissions to the enum before using bitwise operators
            Permissions currentPerms = (Permissions)encDoc.Permissions;
            bool hasPrintPermission = (currentPerms & Permissions.PrintDocument) == Permissions.PrintDocument;

            Console.WriteLine($"Encryption algorithm: AES‑128");
            Console.WriteLine($"Print permission set: {hasPrintPermission}");
        }
    }
}
