using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, Permissions, CryptoAlgorithm)

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string outputPath     = "encrypted.pdf";
        const string ownerPassword  = "ownerSecret";   // password required for editing (owner password)
        const string userPassword   = "";               // empty user password allows viewing without a password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Define permissions: allow printing and content extraction, but do NOT grant any modification rights
                Permissions allowedPermissions = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt the document:
                // - userPassword: empty (viewing is unrestricted)
                // - ownerPassword: required for any editing operations
                // - allowedPermissions: what the viewer may do
                // - CryptoAlgorithm: strongest available (AES‑256)
                doc.Encrypt(userPassword, ownerPassword, allowedPermissions, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF encrypted successfully. Viewable without password, editing requires the owner password.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}