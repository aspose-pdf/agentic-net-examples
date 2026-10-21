using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";
        const string ownerPassword = "ownerPwd";

        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPath}");
            return;
        }

        // Load the encrypted PDF using the owner password.
        Document pdfDoc = new Document(encryptedPath, ownerPassword);

        // Save the document; this creates an unencrypted copy.
        pdfDoc.Save(decryptedPath);

        Console.WriteLine($"Decrypted PDF saved to '{decryptedPath}'.");
    }
}
