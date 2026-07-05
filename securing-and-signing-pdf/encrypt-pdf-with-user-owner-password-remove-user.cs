using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string finalPath = "final_no_user_password.pdf";

        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // -----------------------------------------------------------------
        // 1. Ensure a source PDF exists – create a simple one if missing.
        // -----------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (Document tempDoc = new Document())
            {
                Page page = tempDoc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Sample PDF for encryption demo."));
                tempDoc.Save(inputPath);
            }
        }

        // -----------------------------------------------------------------
        // 2. Load the original PDF (assumed to be unencrypted) and encrypt it
        //    with both user and owner passwords using AES‑256.
        // -----------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // Combine permission flags and cast the result to Permissions.
            Permissions perms = (Permissions)(Permissions.PrintDocument |
                                                Permissions.ModifyContent |
                                                Permissions.ExtractContent |
                                                Permissions.ModifyTextAnnotations |
                                                Permissions.FillForm |
                                                Permissions.AssembleDocument |
                                                Permissions.PrintingQuality);

            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
            doc.Save(encryptedPath);
        }

        // -----------------------------------------------------------------
        // 3. Open the encrypted PDF with the owner password and remove the
        //    user password while keeping the owner password unchanged.
        // -----------------------------------------------------------------
        using (Document encryptedDoc = new Document(encryptedPath, ownerPassword))
        {
            // Retrieve the existing permissions (returned as int) and cast to Permissions.
            Permissions existingPerms = (Permissions)encryptedDoc.Permissions;

            // Re‑encrypt the document with an empty user password.
            encryptedDoc.Encrypt(string.Empty, ownerPassword, existingPerms, CryptoAlgorithm.AESx256);
            encryptedDoc.Save(finalPath);
        }

        Console.WriteLine($"Encryption complete. Encrypted file: {encryptedPath}");
        Console.WriteLine($"User password removed. Final file: {finalPath}");
    }
}
