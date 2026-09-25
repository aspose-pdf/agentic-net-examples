using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string ownerPassword = "ownerSecret";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Empty user password – users can open the file but will be limited by permissions
        string userPassword = "";

        // No permissions for the user (equivalent to "none")
        Permissions userPermissions = (Permissions)0;

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Encrypt with owner password, empty user password, restrictive permissions, AES‑256
                doc.Encrypt(userPassword, ownerPassword, userPermissions, CryptoAlgorithm.AESx256);
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF encrypted with owner password only. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
