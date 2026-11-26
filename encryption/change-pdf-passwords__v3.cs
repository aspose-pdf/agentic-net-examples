using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath   = "encryptedInput.pdf";
        const string outputPath  = "passwordChanged.pdf";
        const string oldOwnerPwd = "owner2";
        const string newUserPwd  = "newUser";
        const string newOwnerPwd = "newOwner";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the existing owner password.
            // Passing the owner password grants full access.
            using (Document doc = new Document(inputPath, oldOwnerPwd))
            {
                // Define desired permissions for the re‑encrypted document.
                // Only enum members that exist in all supported Aspose.Pdf versions are used.
                Permissions perms = Permissions.PrintDocument |
                                     Permissions.ModifyContent |
                                     Permissions.ExtractContent;

                // Re‑encrypt with new user and owner passwords, using AES‑256.
                doc.Encrypt(newUserPwd, newOwnerPwd, perms, CryptoAlgorithm.AESx256);

                // Save the PDF with the updated passwords.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Password changed PDF saved to '{outputPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Failed to open PDF: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
