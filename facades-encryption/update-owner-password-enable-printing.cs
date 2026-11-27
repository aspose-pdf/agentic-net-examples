using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPath = "updated_output.pdf";
        const string userPassword = "user123";
        const string newOwnerPassword = "newOwner456";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF using the current user password
            Document pdfDoc = new Document(inputPath, userPassword);

            // Define the permissions you want to allow (printing in this case)
            Permissions allowedPermissions = Permissions.PrintDocument;

            // Re‑encrypt the document with the same user password, a new owner password and the desired permissions
            pdfDoc.Encrypt(userPassword, newOwnerPassword, allowedPermissions, CryptoAlgorithm.AESx256);

            // Save the updated PDF
            pdfDoc.Save(outputPath);

            Console.WriteLine($"PDF saved with updated security to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
