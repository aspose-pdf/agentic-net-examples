using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";   // Path to the encrypted PDF
        const string password  = "user123";        // User or owner password
        const string outputPath = "decrypted.pdf"; // Where to save the decrypted copy

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the encrypted PDF by supplying the password
        using (Document doc = new Document(inputPath, password))
        {
            // Decrypt the document (Decrypt takes no parameters)
            doc.Decrypt();

            // Save the now‑decrypted PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF opened and saved as '{outputPath}'.");
    }
}