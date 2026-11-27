using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Permissions, CryptoAlgorithm)

class Program
{
    static void Main()
    {
        // Paths
        const string inputPath      = "encrypted_input.pdf";
        const string outputPath     = "reencrypted_output.pdf";

        // Existing passwords (used to open the encrypted PDF)
        const string currentUserPassword  = "oldUserPwd";
        const string currentOwnerPassword = "oldOwnerPwd";

        // New passwords to set
        const string newUserPassword  = "newUserPwd";
        const string newOwnerPassword = "newOwnerPwd";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF by providing the current user password.
            // The owner password is not required for opening; only the user password is needed.
            using (Document doc = new Document(inputPath, currentUserPassword))
            {
                // Determine the original encryption algorithm.
                // Aspose.Pdf does not expose the original algorithm directly,
                // so we assume we want to keep the same key size.
                // Replace CryptoAlgorithm.AESx256 with the appropriate value
                // if you know the original algorithm (e.g., RC4x40, RC4x128, AESx128, AESx256).
                CryptoAlgorithm originalAlgorithm = CryptoAlgorithm.AESx256; // keep original key size

                // Set new passwords, enable printing, and preserve the original algorithm.
                Permissions perms = Permissions.PrintDocument; // enable printing (add other flags if needed)
                doc.Encrypt(newUserPassword, newOwnerPassword, perms, originalAlgorithm);

                // Save the re‑encrypted PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Re‑encrypted PDF saved to '{outputPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Password error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}