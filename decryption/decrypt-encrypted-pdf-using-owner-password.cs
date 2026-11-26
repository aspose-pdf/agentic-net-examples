using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";
        const string ownerPassword = "owner123"; // owner password used to open the file

        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPath}");
            return;
        }

        // Open the encrypted PDF. The password argument accepts either the user or owner password.
        using (Document doc = new Document(encryptedPath, ownerPassword))
        {
            // Decrypt the document. No parameters are required.
            doc.Decrypt();

            // Save the now‑decrypted PDF.
            doc.Save(decryptedPath);
        }

        Console.WriteLine($"Decrypted PDF saved to '{decryptedPath}'.");
    }
}