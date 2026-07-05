using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, Permissions, CryptoAlgorithm)

class Program
{
    static void Main()
    {
        const string inputPath  = "zugferd_input.pdf";   // original ZUGFeRD PDF
        const string outputPath = "zugferd_encrypted.pdf"; // encrypted result

        const string userPassword  = "user123";   // password required to open the PDF
        const string ownerPassword = "owner123";  // password required to change permissions

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (no special load options needed for a standard PDF)
            using (Document doc = new Document(inputPath))
            {
                // Define the permissions you want to allow for the user password.
                // Here we allow printing and content extraction; adjust as needed.
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt the document using the recommended AES‑256 algorithm.
                // This overload matches the rule: Encrypt(string, string, Permissions, CryptoAlgorithm)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF. The Save method writes a PDF regardless of the file extension.
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