using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as requested; not used directly because PdfFileSecurity is obsolete.

class Program
{
    static void Main()
    {
        const string inputPath  = "encrypted_input.pdf";
        const string outputPath = "encrypted_output.pdf";

        // Existing user password required to open the encrypted PDF.
        const string userPassword = "user123";

        // New owner password to replace the original one.
        const string newOwnerPassword = "newOwner456";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF using the user password.
            using (Document doc = new Document(inputPath, userPassword))
            {
                // Preserve encryption strength by using a strong algorithm.
                // Aspose.Pdf does not expose the original algorithm, so we choose AES‑256.
                CryptoAlgorithm algorithm = CryptoAlgorithm.AESx256;

                // Disable printing by not setting the PrintDocument permission.
                // Setting Permissions to 0 removes all permissions, which includes printing.
                Permissions permissions = 0; // No permissions granted.

                // Re‑encrypt the document with the same user password, a new owner password,
                // the reduced permissions, and the chosen encryption algorithm.
                doc.Encrypt(userPassword, newOwnerPassword, permissions, algorithm);

                // Save the modified PDF. The document remains encrypted.
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