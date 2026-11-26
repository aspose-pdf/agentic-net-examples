using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPath = "encrypted.pdf";
        const string ownerPassword  = "owner123";
        const string decryptedPath = "decrypted.pdf";

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
                // Remove encryption from the document
                doc.Decrypt();

                // Save the decrypted PDF
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