using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted_owner_only.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Owner password (full access) – users have no password, thus no permissions.
        const string ownerPassword = "OwnerSecret123";
        const string userPassword  = ""; // empty user password

        // Restrict all user permissions by passing 0 (no flags).
        Permissions userPermissions = 0;

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Encrypt with owner password only.
                doc.Encrypt(userPassword, ownerPassword, userPermissions, CryptoAlgorithm.AESx256);
                doc.Save(outputPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}