using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "protected.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Disable printing by omitting the PrintDocument permission.
                Permissions perms = Permissions.ExtractContent;
                doc.Encrypt("user123", "admin456", perms, CryptoAlgorithm.AESx256);
                doc.Save(outputPath);
            }

            Console.WriteLine($"Protected PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}