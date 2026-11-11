using System;
using System.IO;
using Aspose.Pdf; // Document, Permissions, CryptoAlgorithm

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_privileges.pdf";

        // Owner password is required to enforce permissions.
        // User password left empty to allow opening without a password.
        const string userPassword = "";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Allow printing, disallow copying (ExtractContent).
        Permissions perms = Permissions.PrintDocument;

        // Load the PDF, apply the permissions and save.
        Document doc = new Document(inputPath);
        doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
        doc.Save(outputPath);

        Console.WriteLine($"Privileges set and saved to '{outputPath}'.");
    }
}
