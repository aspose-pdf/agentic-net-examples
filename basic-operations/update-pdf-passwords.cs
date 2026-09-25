using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "protected.pdf";
        const string outputPath     = "updated.pdf";
        const string oldUserPwd     = "oldUser123";
        const string newUserPwd     = "newUser456";
        const string ownerPwd       = "owner789";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the PDF with the existing user password.
            using (Document doc = new Document(inputPath, oldUserPwd))
            {
                // Define desired permissions (adjust as needed).
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Re‑encrypt the document with the new user password.
                // Owner password remains the same; AES‑256 is recommended.
                doc.Encrypt(newUserPwd, ownerPwd, perms, CryptoAlgorithm.AESx256);

                // Save the PDF with the updated credentials.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Password updated and saved to '{outputPath}'.");
        }
        catch (InvalidPasswordException)
        {
            Console.Error.WriteLine("The provided password is incorrect.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}