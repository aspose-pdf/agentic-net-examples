using System;
using System.IO;
using Aspose.Pdf;

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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document document = new Document(inputPath))
            {
                Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;
                document.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);
                document.Save(outputPath);
            }

            Console.WriteLine("Encryption completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
