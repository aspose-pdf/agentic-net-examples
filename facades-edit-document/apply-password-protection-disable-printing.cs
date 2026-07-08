using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "protected.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Define permissions: no privileges (printing disabled and everything else disabled)
        // Using a zero value for the Flags enum disables all permissions.
        Permissions perms = (Permissions)0;

        // Encrypt the PDF with user password, owner password, no permissions, and 256‑bit AES encryption
        // Pass arguments positionally because the overload does not support named parameters for the algorithm.
        doc.Encrypt("user123", "admin456", perms, CryptoAlgorithm.AESx256);

        // Save the protected PDF
        doc.Save(outputPath);

        Console.WriteLine($"PDF encrypted successfully: {outputPath}");
    }
}