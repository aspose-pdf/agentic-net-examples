using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the encrypted PDF file
        const string inputPath = "encrypted.pdf";
        // Password required to open the document (user password)
        const string password = "user123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the encrypted PDF using the constructor that accepts a password
        // This follows the standard Document creation pattern.
        using (Document doc = new Document(inputPath, password))
        {
            // The document is now decrypted in memory and can be used normally.
            Console.WriteLine($"Successfully opened encrypted PDF.");
            Console.WriteLine($"Page count: {doc.Pages.Count}");

            // Example: save a copy of the opened document (optional)
            const string outputPath = "decrypted_copy.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"Decrypted copy saved to '{outputPath}'.");
        }
    }
}