using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // No permissions granted – editing and printing are disabled
                Permissions permissions = (Permissions)0; // equivalent to no permissions
                doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx128);
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF encrypted with 128‑bit AES and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
