using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths for the encrypted input PDF and the decrypted output PDF
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";

        // The user password required to open the encrypted PDF
        const string userPassword = "user123";

        // Verify that the encrypted file exists
        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPath}");
            return;
        }

        try
        {
            // Load the encrypted document using the user password
            using (Document doc = new Document(encryptedPath, userPassword))
            {
                // Decrypt the document (no parameters required)
                doc.Decrypt();

                // Save the unprotected version to a new file
                doc.Save(decryptedPath);
            }

            Console.WriteLine($"Decryption successful. Saved to '{decryptedPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            // Thrown when the provided password is incorrect
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}