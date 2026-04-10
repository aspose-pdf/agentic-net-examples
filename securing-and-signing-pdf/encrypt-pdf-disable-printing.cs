using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Open the source PDF inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPath))
            {
                // Define permissions without the PrintDocument flag to disable printing.
                // Here we allow modifying content and extracting content, but you can set
                // Permissions perms = 0; to disable all optional actions.
                Permissions perms = Permissions.ModifyContent |
                                    Permissions.ExtractContent |
                                    Permissions.ModifyTextAnnotations |
                                    Permissions.FillForm |
                                    Permissions.AssembleDocument;

                // Encrypt using the recommended AES‑256 algorithm.
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF.
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