using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as requested

class Program
{
    static void Main()
    {
        const string inputPath          = "encrypted_input.pdf";
        const string outputPath         = "reprotected_output.pdf";
        const string currentUserPassword = "oldUserPassword";
        const string newUserPassword     = "newUserPassword";
        const string newOwnerPassword    = "newOwnerPassword";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF using the current user password
            using (Document doc = new Document(inputPath, currentUserPassword))
            {
                // Disable printing by omitting the PrintDocument permission
                Permissions perms = Permissions.ExtractContent; // add other permissions as needed

                // Keep the original encryption key size.
                // Replace the placeholder with the actual algorithm if it is known.
                CryptoAlgorithm originalAlgorithm = CryptoAlgorithm.AESx256;

                // Apply new passwords and updated permissions
                doc.Encrypt(newUserPassword, newOwnerPassword, perms, originalAlgorithm);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'.");
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