using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "protected.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Attempt to open a password‑protected PDF without providing a password.
            // This will throw InvalidPasswordException if the document is encrypted.
            using (Document doc = new Document(inputPath))
            {
                // If the document opens (unlikely), display basic info.
                Console.WriteLine($"Document opened. Page count: {doc.Pages.Count}");
            }
        }
        catch (InvalidPasswordException ex)
        {
            // Specific handling for missing/incorrect password.
            Console.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}