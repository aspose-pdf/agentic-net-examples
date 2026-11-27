using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";
        const string ownerPassword = "owner123";

        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPath}");
            return;
        }

        // Load the encrypted PDF by supplying the owner password.
        // The Document constructor that takes (string path, string password) will
        // decrypt the file in memory.
        Document doc = new Document(encryptedPath, ownerPassword);

        // Save the document without any encryption.
        doc.Save(decryptedPath);

        Console.WriteLine($"Decrypted PDF saved to '{decryptedPath}'.");
    }
}
