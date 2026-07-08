using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";
        const string userPassword   = "user123";

        // Verify the encrypted file exists
        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF with the user password
            using (Document doc = new Document(encryptedPath, userPassword))
            {
                // Decrypt the document (no parameters required)
                doc.Decrypt();

                // Save the unprotected version
                doc.Save(decryptedPath);
            }

            Console.WriteLine($"Decrypted PDF saved to '{decryptedPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            // Thrown when the supplied password is incorrect
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}