using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the encrypted output PDF
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted.pdf";

        // Passwords: userPassword is required to open the document,
        // ownerPassword is required to change permissions or edit the document.
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Define the permissions that will be allowed after opening with the user password.
                // Here we allow printing, modifying content, editing annotations, filling forms,
                // and assembling the document. Adjust as needed.
                Permissions perms = Permissions.PrintDocument |
                                    Permissions.ModifyContent |
                                    Permissions.ModifyTextAnnotations |
                                    Permissions.FillForm |
                                    Permissions.AssembleDocument;

                // Encrypt the document using the user and owner passwords.
                // CryptoAlgorithm.AESx256 is the recommended algorithm.
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF. The Save method writes the encrypted content to the file.
                doc.Save(encryptedPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{encryptedPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}