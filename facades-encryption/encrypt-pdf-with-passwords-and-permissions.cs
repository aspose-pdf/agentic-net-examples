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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPath);

            // Combine all available permissions (since Permissions.All does not exist in this version)
            Permissions allPermissions = Permissions.PrintDocument |
                                         Permissions.ModifyContent |
                                         Permissions.ExtractContent |
                                         Permissions.ModifyTextAnnotations |
                                         Permissions.FillForm |
                                         Permissions.ExtractContentWithDisabilities |
                                         Permissions.AssembleDocument |
                                         Permissions.PrintingQuality;

            // Apply AES‑256 encryption with the specified user and owner passwords
            pdfDoc.Encrypt(userPassword, ownerPassword, allPermissions, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            pdfDoc.Save(outputPath);

            Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
