using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "protected.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Attempt to open the PDF without providing a password.
            // The Document constructor will throw InvalidPasswordException
            // if the file is password‑protected.
            using (Document doc = new Document(pdfPath))
            {
                // If the file opens successfully (unlikely in this scenario),
                // you can access its properties here.
                Console.WriteLine($"Pages: {doc.Pages.Count}");
            }
        }
        catch (InvalidPasswordException ex)
        {
            // Handle the specific exception for missing or wrong password.
            Console.Error.WriteLine("Failed to open PDF: password required.");
            Console.Error.WriteLine($"Error details: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Fallback for any other unexpected errors.
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
