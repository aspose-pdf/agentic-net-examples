using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted_view_only.pdf";

        // Password for editing (owner password). Empty user password allows viewing without a password.
        const string userPassword  = "";          // No password required to open/view the PDF
        const string ownerPassword = "EditOnly123"; // Password required for editing operations

        // Permissions: allow viewing, printing and content extraction, but disallow modifications.
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF, encrypt it, and save the result.
            using (Document doc = new Document(inputPath))
            {
                // Encrypt using AES-256. The user password is empty, so opening the PDF requires no password.
                // The owner password protects editing operations.
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                doc.Save(outputPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}