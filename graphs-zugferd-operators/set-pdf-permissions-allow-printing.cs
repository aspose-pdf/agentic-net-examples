using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "restricted.pdf";

        // Permissions: allow printing only.
        // Do NOT include ExtractContent (copy) or ModifyAnnotations.
        Permissions perms = Permissions.PrintDocument;

        // Empty user password means anyone can open the file,
        // but the owner password protects the permission settings.
        const string userPassword  = "";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF, apply permissions via encryption, and save.
            using (Document doc = new Document(inputPath))
            {
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with permissions to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}