using System;
using System.IO;
using Aspose.Pdf;

class EncryptPdf
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";

        // User (open) password and owner (full‑access) password
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Define permissions – omit PrintDocument to restrict printing
                Permissions perms = Permissions.ModifyContent |
                                    Permissions.ExtractContent |
                                    Permissions.ModifyTextAnnotations |
                                    Permissions.FillForm |
                                    Permissions.AssembleDocument |
                                    Permissions.PrintingQuality;

                // Encrypt the document using the recommended CryptoAlgorithm (AES‑256)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

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