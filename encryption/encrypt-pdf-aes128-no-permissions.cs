using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "user5AES.pdf";
        const string userPassword = "user5";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Open the PDF with the provided user password
            using (Document doc = new Document(inputPath, userPassword))
            {
                // No privileges: set permissions to zero (none)
                Permissions perms = (Permissions)0; // use Permissions.None if available

                // Encrypt using AES 128-bit; owner password left empty
                doc.Encrypt(userPassword, string.Empty, perms, CryptoAlgorithm.AESx128);

                // Save the encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Encrypted PDF saved as '{outputPath}'.");
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