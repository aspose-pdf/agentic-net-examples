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
            // Attempt to open the PDF without supplying a password
            using (Document doc = new Document(inputPath))
            {
                // If the document opens, display basic info
                Console.WriteLine($"PDF opened successfully. Pages: {doc.Pages.Count}");
            }
        }
        catch (InvalidPasswordException ex)
        {
            // Expected when the PDF is password‑protected and no password is provided
            Console.WriteLine($"Invalid password error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Any other unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}