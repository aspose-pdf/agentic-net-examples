using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";

        // Password required to open the document (user password).
        const string userPassword = "viewOnly123";
        // Owner password – can be the same as user password or a different secret.
        const string ownerPassword = "ownerSecret";

        // Permissions: allow printing only; editing and copying are not permitted.
        Permissions perms = Permissions.PrintDocument;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF.
            using (Document doc = new Document(inputPath))
            {
                // Encrypt with user password, owner password, desired permissions, and AES‑256 algorithm.
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF.
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
