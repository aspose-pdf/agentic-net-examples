using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (contains Document, InvalidPasswordException, etc.)

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
            // Attempt to open the PDF without providing a password.
            // This will throw InvalidPasswordException if the document is encrypted.
            using (Document doc = new Document(inputPath))
            {
                // If the document opens successfully, you can work with it here.
                Console.WriteLine($"Pages: {doc.Pages.Count}");
            }
        }
        catch (InvalidPasswordException ex)
        {
            // Handle the specific case where the PDF is password‑protected.
            Console.Error.WriteLine("The PDF is password protected and no password was supplied.");
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors.
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}