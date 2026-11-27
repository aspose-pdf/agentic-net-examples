using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // No permissions – equivalent to DocumentPrivilege.ForbidAll
        Permissions permissions = (Permissions)0; // or Permissions.None if using a newer version that defines it

        // Encrypt the document using AES‑256
        doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

        // Save the encrypted PDF
        doc.Save(outputPath);

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}