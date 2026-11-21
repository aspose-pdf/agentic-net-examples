using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security; // Required for Permissions enum

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted_formfill.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF and apply encryption with specific permissions.
            using (Document doc = new Document(inputPath))
            {
                // Allow only form filling (ModifyContent) and disable all other actions,
                // including content extraction (CopyContents).
                Permissions perms = Permissions.ModifyContent; // form filling permission
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
