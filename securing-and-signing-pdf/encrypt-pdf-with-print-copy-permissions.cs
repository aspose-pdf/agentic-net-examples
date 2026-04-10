using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace contains Document, Permissions, CryptoAlgorithm

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";

        // Passwords for encryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document doc = new Document(inputPath))
            {
                // Set permissions: allow printing and copying (extracting content)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt the document using the recommended CryptoAlgorithm (AESx256)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF (lifecycle rule: use Document.Save)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Encryption successful. Encrypted file saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during encryption: {ex.Message}");
        }
    }
}