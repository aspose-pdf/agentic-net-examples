using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPath = "re_encrypted_output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF using the user password
            using (Document doc = new Document(inputPath, userPassword))
            {
                // Example permissions: allow printing and content extraction
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Re‑encrypt with AES‑128 while preserving the original passwords
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx128);

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