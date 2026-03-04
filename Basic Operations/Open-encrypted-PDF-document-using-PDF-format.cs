using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "encrypted.pdf";   // Path to the encrypted PDF
        const string password   = "user123";        // User or owner password
        const string outputPath = "decrypted.pdf";  // Where to save the opened document

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the password
            using (Document doc = new Document(inputPath, password))
            {
                // Example operation: display the number of pages
                Console.WriteLine($"Pages: {doc.Pages.Count}");

                // Save the document (can be the same file or a new one)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}