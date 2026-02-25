using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPath = "encrypted.pdf";
        const string userPassword  = "user123";

        // Verify the encrypted file exists
        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the user password.
            // The Document constructor (string, string) handles encrypted files.
            using (Document doc = new Document(encryptedPath, userPassword))
            {
                Console.WriteLine($"Encrypted PDF opened successfully. Page count: {doc.Pages.Count}");

                // Decrypt the document (no parameters required) and save a plain copy.
                doc.Decrypt();
                doc.Save("decrypted.pdf");
                Console.WriteLine("Decrypted PDF saved as 'decrypted.pdf'.");
            }
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