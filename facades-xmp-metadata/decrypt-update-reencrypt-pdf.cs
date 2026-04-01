using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "reprotected.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the password‑protected PDF using the user password
            using (Document doc = new Document(inputPath, userPassword))
            {
                // Decrypt the document (removes encryption but keeps content)
                doc.Decrypt();

                // Update the Creator metadata field (CreatorTool does not exist; use Creator)
                doc.Info.Creator = "MyApp v1.0";

                // Define desired permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Re‑encrypt the PDF with AES‑256 encryption
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the re‑encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed PDF saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
