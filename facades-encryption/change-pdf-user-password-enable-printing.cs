using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Facades;            // Facades namespace (required by task)

class Program
{
    static void Main()
    {
        // Paths
        const string inputPath  = "encrypted_input.pdf";
        const string outputPath = "reencrypted_output.pdf";

        // Existing passwords
        const string oldUserPassword = "oldUserPass";
        const string ownerPassword   = "ownerPass";

        // New user password to set
        const string newUserPassword = "newUserPass";

        // Verify input file exists
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
                // Permissions to enable printing (add other flags as needed)
                Aspose.Pdf.Permissions perms = Aspose.Pdf.Permissions.PrintDocument;

                // NOTE: To keep the original encryption key size, use the same
                // CryptoAlgorithm that was used originally. Replace AESx128 with
                // the appropriate value (e.g., AESx256) if required.
                Aspose.Pdf.CryptoAlgorithm algorithm = Aspose.Pdf.CryptoAlgorithm.AESx128;

                // Re‑encrypt the document with the new user password,
                // preserving the owner password, permissions and algorithm.
                doc.Encrypt(newUserPassword, ownerPassword, perms, algorithm);

                // Save the updated PDF. The document remains encrypted.
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