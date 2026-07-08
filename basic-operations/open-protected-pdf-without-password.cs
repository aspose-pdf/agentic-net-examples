using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document and InvalidPasswordException

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
            // If the document is encrypted, an InvalidPasswordException will be thrown.
            using (Document doc = new Document(pdfPath))
            {
                // If opening succeeds, you can work with the document here.
                Console.WriteLine($"Opened PDF successfully. Pages: {doc.Pages.Count}");
            }
        }
        catch (InvalidPasswordException ex)
        {
            // Handle the case where the PDF is password‑protected.
            Console.Error.WriteLine("Failed to open PDF: invalid or missing password.");
            Console.Error.WriteLine($"Error details: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors.
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}