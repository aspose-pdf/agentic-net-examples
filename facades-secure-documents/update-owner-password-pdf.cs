using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "protected.pdf";
        const string outputPath = "updated_owner.pdf";
        const string userPassword = "user123";
        const string newOwnerPassword = "newOwner456";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the protected PDF using the existing user password
            using (Document doc = new Document(inputPath, userPassword))
            {
                // Preserve the current permissions (no change to privileges)
                // Document.Permissions may return an int in some Aspose.Pdf versions, so cast it to Permissions.
                Permissions currentPermissions = (Permissions)doc.Permissions;

                // Re‑encrypt with the same user password, a new owner password,
                // and the unchanged permissions. Prefer AES‑256 encryption.
                doc.Encrypt(userPassword, newOwnerPassword, currentPermissions, CryptoAlgorithm.AESx256);

                // Save the PDF with the updated owner password
                doc.Save(outputPath);
            }

            Console.WriteLine($"Owner password updated successfully. Output saved to '{outputPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
