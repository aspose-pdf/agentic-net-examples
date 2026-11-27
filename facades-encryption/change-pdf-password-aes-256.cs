using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPath = "reprotected_output.pdf";
        const string currentUserPassword = "oldUserPwd";
        const string newUserPassword = "newUserPwd123";
        const string newOwnerPassword = "newOwnerPwd123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing encrypted PDF using the current user password.
            Document pdfDoc = new Document(inputPath, currentUserPassword);

            // Enable printing and apply 256‑bit AES encryption with the new passwords.
            Permissions perms = Permissions.PrintDocument; // add other flags if needed, e.g., | Permissions.ModifyContent
            pdfDoc.Encrypt(newUserPassword, newOwnerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the updated PDF.
            pdfDoc.Save(outputPath);

            Console.WriteLine($"PDF saved with new passwords to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
