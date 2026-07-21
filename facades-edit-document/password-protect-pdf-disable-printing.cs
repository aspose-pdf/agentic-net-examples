using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "protected.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "admin456";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Define permissions – allow everything except printing
        Permissions perms = Permissions.ModifyContent |
                             Permissions.ExtractContent |
                             Permissions.FillForm |
                             Permissions.AssembleDocument;

        // Encrypt the document with user and owner passwords, 256‑bit AES, and the defined permissions
        doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

        // Save the protected PDF
        doc.Save(outputPath);

        Console.WriteLine($"PDF encrypted successfully: {outputPath}");
    }
}