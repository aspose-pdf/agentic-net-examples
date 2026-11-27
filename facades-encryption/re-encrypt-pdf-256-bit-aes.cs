using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace included as requested

class Program
{
    static void Main()
    {
        const string inputPath  = "encrypted_input.pdf";
        const string outputPath = "re_encrypted_output.pdf";

        // Existing passwords (user password is required to open the file)
        const string oldUserPassword  = "oldUserPwd";
        const string oldOwnerPassword = "oldOwnerPwd"; // not needed for opening, kept for reference

        // New passwords to be set
        const string newUserPassword  = "newUserPwd123";
        const string newOwnerPassword = "newOwnerPwd123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF using the current user password
            using (Document doc = new Document(inputPath, oldUserPassword))
            {
                // Set desired permissions – allow printing only
                Permissions perms = Permissions.PrintDocument;

                // Apply 256‑bit AES encryption with the new passwords
                doc.Encrypt(newUserPassword, newOwnerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the re‑encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Re‑encrypted PDF saved to '{outputPath}'.");
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