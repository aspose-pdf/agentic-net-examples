using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as per requirement

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPath = "output.pdf";
        const string userPassword = "user123";
        const string newOwnerPassword = "newOwner456";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF using the existing user password
            using (Document doc = new Document(inputPath, userPassword))
            {
                // Define permissions (retain typical permissions)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Re‑encrypt with the same user password and a new owner password
                doc.Encrypt(userPassword, newOwnerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Owner password changed and saved to '{outputPath}'.");
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