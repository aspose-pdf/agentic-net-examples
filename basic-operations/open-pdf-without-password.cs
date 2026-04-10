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
            // Attempt to open the PDF without providing a password.
            using (Document doc = new Document(inputPath))
            {
                // If the document opens, it is not password‑protected.
                Console.WriteLine($"PDF opened successfully. Pages: {doc.Pages.Count}");
            }
        }
        catch (InvalidPasswordException ex)
        {
            // Handle the case where the PDF is encrypted and no (or wrong) password was supplied.
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}