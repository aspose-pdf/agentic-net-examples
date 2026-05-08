using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (contains Document, Permissions, CryptoAlgorithm)

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the encrypted output PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";

        // Passwords: user password (required to open) and owner password (required to change permissions)
        const string userPassword  = "user123";
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
                // Here we allow printing but disallow content modification (editing).
                Permissions permissions = Permissions.PrintDocument;

                // Encrypt the document:
                // - userPassword: required to open the PDF
                // - ownerPassword: required to change permissions or remove encryption
                // - permissions: set of allowed actions for the user password
                // - CryptoAlgorithm.AESx256: strong encryption algorithm
                doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF. The Save method writes the PDF regardless of the file extension.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}