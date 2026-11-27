using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPath = "updated_output.pdf";
        const string currentUserPassword = "oldPass";
        const string newUserPassword = "newPass";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF using the existing user password
            Document doc = new Document(inputPath, currentUserPassword);

            // Define the permissions you want to allow – in this case printing
            Permissions permissions = Permissions.PrintDocument;

            // Re‑encrypt the document with the new user password and the desired permissions.
            // Owner password is left empty (you can set a strong owner password if required).
            // AES‑256 is used for strong encryption.
            doc.Encrypt(newUserPassword, "", permissions, CryptoAlgorithm.AESx256);

            // Save the modified PDF
            doc.Save(outputPath);

            Console.WriteLine($"PDF saved to '{outputPath}' with new password and printing allowed.");
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
