using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "signed_input.pdf";
        const string outputPath = "privileged_output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the signed PDF
            using (Document doc = new Document(inputPath))
            {
                // Set permissions: allow printing, but do NOT allow content extraction or form filling
                Permissions perms = Permissions.PrintDocument; // only printing is permitted

                // Encrypt with AES‑256 and apply the permissions
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the protected PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Privileges applied and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}