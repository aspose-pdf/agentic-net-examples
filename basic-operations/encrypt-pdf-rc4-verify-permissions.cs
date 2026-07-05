using System;
using System.IO;
using Aspose.Pdf;               // Core API namespace
using Aspose.Pdf.Text;          // For Permissions enum (if needed)

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted.pdf";
        const string decryptedPath  = "decrypted.pdf";

        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the source PDF and encrypt it using RC4 (128‑bit) algorithm.
        //    No copying permission is granted – we set Permissions to 0
        //    (or only allow printing if desired).  The CryptoAlgorithm
        //    enum must be used; CryptographicStandard is NOT valid.
        // -----------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // Permissions = 0 means no special rights (no printing, no copying, etc.)
            Permissions noPermissions = (Permissions)0;

            // Encrypt with RC4 128‑bit algorithm
            doc.Encrypt(userPassword, ownerPassword, noPermissions, CryptoAlgorithm.RC4x128);

            // Save the encrypted PDF
            doc.Save(encryptedPath);
        }

        Console.WriteLine($"Document encrypted and saved to '{encryptedPath}'.");

        // -----------------------------------------------------------------
        // 2. Verify the encryption by opening the file with the user password.
        //    If the password is correct the document can be opened; otherwise
        //    an exception will be thrown.  After opening we decrypt it to
        //    demonstrate that the document was indeed encrypted.
        // -----------------------------------------------------------------
        try
        {
            using (Document encryptedDoc = new Document(encryptedPath, userPassword))
            {
                // Decrypt the document (no parameters required)
                encryptedDoc.Decrypt();

                // Save the decrypted version (optional verification step)
                encryptedDoc.Save(decryptedPath);
            }

            Console.WriteLine($"Encryption verified. Decrypted copy saved to '{decryptedPath}'.");
        }
        catch (InvalidPasswordException)
        {
            Console.Error.WriteLine("Failed to open the encrypted PDF – password is incorrect.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while verifying encryption: {ex.Message}");
        }
    }
}