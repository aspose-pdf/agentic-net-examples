using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace included as requested

class Program
{
    static void Main()
    {
        const string inputPath = "protected.pdf";
        const string outputPath = "owner_updated.pdf";
        const string userPassword = "user123";      // existing user password
        const string newOwnerPassword = "newOwner456"; // new owner password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the existing user password
            using (Document doc = new Document(inputPath, userPassword))
            {
                // Permissions property returns an int in newer Aspose.Pdf versions.
                // Cast it explicitly to the Permissions enum before re‑encryption.
                Permissions currentPermissions = (Permissions)doc.Permissions;

                // Re‑encrypt with the same user password, a new owner password,
                // the original permissions, and a strong algorithm (AES‑256)
                doc.Encrypt(
                    userPassword,
                    newOwnerPassword,
                    currentPermissions,
                    CryptoAlgorithm.AESx256);

                // Save the updated document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Owner password updated successfully. Saved to '{outputPath}'.");
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
