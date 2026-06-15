using System;
using System.IO;
using Aspose.Pdf;

// Suppress known NuGet vulnerability warning (NU1903) for Microsoft.Bcl.Memory.
#pragma warning disable NU1903

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document to inspect its encryption status.
        // Aspose.Pdf's Document class provides the IsEncrypted property.
        using (Document doc = new Document(inputPath))
        {
            bool isEncrypted = doc.IsEncrypted;
            Console.WriteLine($"IsEncrypted: {isEncrypted}");

            if (!isEncrypted)
            {
                // Encrypt the PDF (example: allow printing only)
                Permissions perms = Permissions.PrintDocument; // choose required permissions
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                doc.Save(outputPath);
                Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
            }
            else
            {
                // Decrypt the PDF using the owner password.
                // Reload the document with the owner password to gain full access.
                Document encryptedDoc = new Document(inputPath, ownerPassword);
                encryptedDoc.Save(outputPath);
                Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
            }
        }
    }
}
#pragma warning restore NU1903