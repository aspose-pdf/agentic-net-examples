using System;
using System.IO;
using Aspose.Pdf;

class DecryptPdf
{
    static void Main()
    {
        // Paths for the encrypted input PDF and the decrypted output PDF
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";

        // Owner password gives full access to the document
        const string ownerPassword = "owner123";

        // Verify that the input file exists
        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the owner password
            using (Document doc = new Document(encryptedPath, ownerPassword))
            {
                // Decrypt the document (no parameters)
                doc.Decrypt();

                // Save the decrypted version (overwrites or creates a new file)
                doc.Save(decryptedPath);
            }

            Console.WriteLine($"Decrypted PDF saved to '{decryptedPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            // Thrown when the supplied password is incorrect or insufficient
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}