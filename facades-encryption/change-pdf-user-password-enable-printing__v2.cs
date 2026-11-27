using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPath = "updated_output.pdf";
        const string oldUserPassword = "oldPass";
        const string newUserPassword = "newPass";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF using the existing user password
            Document doc = new Document(inputPath, oldUserPassword);

            // Define the permissions you want to allow (printing in this case)
            Permissions perms = Permissions.PrintDocument;

            // Re‑encrypt the document with the new user password and the desired permissions
            // Owner password is set to null (no owner password) and AES‑256 is used for encryption
            doc.Encrypt(newUserPassword, null, perms, CryptoAlgorithm.AESx256);

            // Save the updated PDF
            doc.Save(outputPath);

            Console.WriteLine($"PDF saved with new password and print permission: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
