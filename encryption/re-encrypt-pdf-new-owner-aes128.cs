using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string outputPath     = "reEncryptDiffOwner.pdf";
        const string userPassword   = "userOld2";
        const string newOwnerPassword = "ownerNew2";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the existing PDF using the current user password
            using (Document doc = new Document(inputPath, userPassword))
            {
                // Define desired permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Re‑encrypt the document with the same user password, a new owner password,
                // and AES‑128 encryption algorithm
                doc.Encrypt(userPassword, newOwnerPassword, perms, CryptoAlgorithm.AESx128);

                // Save the re‑encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Re‑encrypted PDF saved to '{outputPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}