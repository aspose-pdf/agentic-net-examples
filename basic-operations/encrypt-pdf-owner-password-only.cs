using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";

        // Owner password gives full access; user password is empty (no user password)
        const string userPassword = "";
        const string ownerPassword = "owner123";

        // Restrict all user permissions (owner can still modify with owner password)
        // The Permissions enum does not define a 'None' member; use a cast to zero to represent no permissions.
        Permissions userPermissions = (Permissions)0;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Encrypt with owner password only, using AES-256 algorithm
                doc.Encrypt(userPassword, ownerPassword, userPermissions, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
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
