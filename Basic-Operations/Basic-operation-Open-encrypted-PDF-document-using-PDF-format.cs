using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPath = "encrypted.pdf";
        const string password       = "user123";
        const string outputPath     = "decrypted.pdf";

        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the user password.
            // The Document constructor with (string filename, string password) handles encrypted files.
            using (Document doc = new Document(encryptedPath, password))
            {
                // Optional: remove encryption from the document.
                // Decrypt() works on a document opened with a password.
                doc.Decrypt();

                // Save the decrypted version.
                doc.Save(outputPath);
                Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}