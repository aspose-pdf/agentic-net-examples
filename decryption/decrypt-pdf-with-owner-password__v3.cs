using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPath = "encrypted.pdf";
        const string outputPath = "decrypted1.pdf";
        const string ownerPassword = "owner1";

        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the owner password.
            using (Document doc = new Document(encryptedPath, ownerPassword))
            {
                // Decrypt removes all security restrictions.
                doc.Decrypt();

                // Save the unprotected PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}