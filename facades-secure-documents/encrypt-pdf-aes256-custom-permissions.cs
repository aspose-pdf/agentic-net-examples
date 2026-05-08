using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and apply AES‑256 encryption with custom privileges
        using (Document doc = new Document(inputPath))
        {
            // Define the required privileges: allow printing & copying (content extraction)
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the document – AES‑256 algorithm
            doc.Encrypt(userPassword: "user123", ownerPassword: "owner123", permissions: permissions, cryptoAlgorithm: CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"File encrypted successfully to '{outputPath}'.");
    }
}
