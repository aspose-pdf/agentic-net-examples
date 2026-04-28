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
            // Attempt to open the PDF without a password.
            // If the document is encrypted, an InvalidPasswordException will be thrown.
            using (Document doc = new Document(inputPath))
            {
                // Document opened successfully (unlikely for a protected PDF).
                Console.WriteLine($"Document opened. Page count: {doc.Pages.Count}");
            }
        }
        catch (InvalidPasswordException ex)
        {
            // Handle the specific case where the PDF requires a password.
            Console.Error.WriteLine($"Invalid password error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}