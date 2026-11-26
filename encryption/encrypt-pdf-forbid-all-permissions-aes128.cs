using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_encrypted.pdf";
        const string ownerPassword = "owner123";
        const string userPassword = "user123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the PDF using the owner password
            using (Document doc = new Document(inputPath, ownerPassword))
            {
                // Enable PDF/UA (screen‑reader) support if the property exists.
                // In recent Aspose.PDF versions the Document class exposes a boolean ScreenReader property.
                // If the property is not available in the referenced version, this line can be safely removed.
                // doc.ScreenReader = true;

                // Forbid all document privileges – achieved by passing a Permissions value with no bits set.
                Permissions perms = (Permissions)0; // No permissions granted to the user.

                // Encrypt the document with AES‑128 using the supplied passwords.
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
