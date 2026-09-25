using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";
        const string userPassword  = "user123";

        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF, supplying the user password
            using (Document doc = new Document(encryptedPath, userPassword))
            {
                // Remove encryption; Decrypt takes no arguments
                doc.Decrypt();

                // Save the unprotected PDF
                doc.Save(decryptedPath);
            }

            Console.WriteLine($"Decrypted PDF saved to '{decryptedPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}