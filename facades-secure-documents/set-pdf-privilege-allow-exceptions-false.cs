using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "privileged.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using Document (the recommended way for modern Aspose.Pdf versions).
        using (Document doc = new Document(inputPath))
        {
            // Define desired permissions.
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Apply encryption / privilege modification. Exceptions will propagate naturally.
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Privilege‑modified PDF saved to '{outputPath}'.");
    }
}
