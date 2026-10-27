using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPath = "protected.pdf";
        const string ownerPassword  = "owner123";
        const string outputPath     = "decrypted.pdf";

        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPath}");
            return;
        }

        // Open the encrypted PDF using the owner password.
        // The password argument accepts either user or owner password.
        using (Document doc = new Document(encryptedPath, ownerPassword))
        {
            // Remove encryption. Decrypt() takes no parameters.
            doc.Decrypt();

            // Save the now‑unencrypted PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
    }
}