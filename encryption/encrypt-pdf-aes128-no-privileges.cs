using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "noPrivAES.pdf";
        const string userPassword = "u3";
        const string ownerPassword = "o3";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Preserve default permissions (all allowed)
                Permissions perms = Permissions.PrintDocument |
                                    Permissions.ModifyContent |
                                    Permissions.ExtractContent |
                                    Permissions.ModifyTextAnnotations |
                                    Permissions.FillForm |
                                    Permissions.ExtractContentWithDisabilities |
                                    Permissions.AssembleDocument |
                                    Permissions.PrintingQuality;

                // Encrypt with AES-128 using the specified user and owner passwords
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx128);

                // Save the encrypted PDF
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
