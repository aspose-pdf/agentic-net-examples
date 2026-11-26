using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "noPrivRC4.pdf";
        const string userPassword = "u4";
        const string ownerPassword = "o4";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Allow all permissions (no restriction) by combining all available flags
                Permissions perms = Permissions.PrintDocument |
                                    Permissions.ModifyContent |
                                    Permissions.ExtractContent |
                                    Permissions.ModifyTextAnnotations |
                                    Permissions.FillForm |
                                    Permissions.ExtractContentWithDisabilities |
                                    Permissions.AssembleDocument |
                                    Permissions.PrintingQuality;

                // Encrypt with RC4 128‑bit algorithm
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.RC4x128);

                // Save the encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Encrypted PDF saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
