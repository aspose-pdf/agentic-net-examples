using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "filled.pdf";
        const string outputPath = "protected.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                doc.Save(outputPath);
            }

            Console.WriteLine($"Password protected PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
