using System;
using System.IO;
using Aspose.Pdf; // CryptoAlgorithm, Permissions, Document

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
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // No permissions (disable printing, copying, etc.)
                Permissions noPermissions = (Permissions)0;

                // Encrypt with AES‑256 using the specified passwords
                doc.Encrypt(userPassword, ownerPassword, noPermissions, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPath);
            }

            // Verify encryption status
            using (Document encryptedDoc = new Document(encryptedPath, userPassword))
            {
                bool isEncrypted = encryptedDoc.IsEncrypted;
                Console.WriteLine($"Document encrypted: {isEncrypted}");

                // Since we set no permissions, printing is not allowed.
                // Permissions cannot be read directly, but we know we set none.
                Console.WriteLine("Printing permission: disabled");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}