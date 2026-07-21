using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";

        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Encrypt the PDF with RC4 (128‑bit) and disable all permissions
        // (i.e., no copying, printing, etc.).
        // -----------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // No permissions granted – cast 0 to the Flags enum.
            Permissions noPermissions = (Permissions)0;

            // Encrypt using RC4 128‑bit algorithm.
            doc.Encrypt(userPassword, ownerPassword, noPermissions, CryptoAlgorithm.RC4x128);

            // Save the encrypted document.
            doc.Save(outputPath);
        }

        // -----------------------------------------------------------------
        // Verify the security settings:
        //   1. Opening without a password should fail.
        //   2. Opening with the correct user password should succeed.
        // -----------------------------------------------------------------
        // 1. Attempt to open without a password – expect InvalidPasswordException.
        try
        {
            using (Document _ = new Document(outputPath))
            {
                // If we reach here, the document was not protected as expected.
                Console.Error.WriteLine("Verification failed: document opened without a password.");
            }
        }
        catch (InvalidPasswordException)
        {
            Console.WriteLine("Verification passed: opening without a password throws InvalidPasswordException.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error during verification (no password): {ex.Message}");
        }

        // 2. Open with the correct user password and ensure we can read the page count.
        try
        {
            using (Document encryptedDoc = new Document(outputPath, userPassword))
            {
                // Decrypt is optional here; the document is already opened with the password.
                // encryptedDoc.Decrypt(); // not required for verification

                Console.WriteLine($"Verification passed: document opened with user password. Page count = {encryptedDoc.Pages.Count}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Verification failed: could not open with user password. {ex.Message}");
        }
    }
}