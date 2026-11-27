using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPath = "reprotected_output.pdf";
        const string oldUserPassword = "oldPass";   // current user password
        const string newUserPassword = "newPass";   // password we want to set

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF with the existing user password.
            // The Document constructor automatically decrypts the file.
            var doc = new Document(inputPath, oldUserPassword);

            // Preserve the current permissions (printing permission stays unchanged).
            // Document.Permissions returns an int; cast it to the Permissions enum.
            Permissions currentPermissions = (Permissions)doc.Permissions;

            // NOTE: Aspose.Pdf does not expose the owner password after opening.
            // If the owner password is known, replace the placeholder below with it.
            // For many scenarios the owner password is the same as the user password.
            string ownerPassword = oldUserPassword; // <-- replace if different

            // Re‑encrypt the PDF with the new user password, same owner password,
            // unchanged permissions and AES‑256 encryption.
            doc.Encrypt(newUserPassword, ownerPassword, currentPermissions, CryptoAlgorithm.AESx256);

            // Save the updated PDF.
            doc.Save(outputPath);

            Console.WriteLine($"User password updated and PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
