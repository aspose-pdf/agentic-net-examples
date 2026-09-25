using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string protectedPdfPath = "protected.pdf";

        if (!File.Exists(protectedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {protectedPdfPath}");
            return;
        }

        try
        {
            // Attempt to open the password‑protected PDF without supplying a password.
            // This will throw InvalidPasswordException if the document is encrypted.
            using (Document doc = new Document(protectedPdfPath))
            {
                // If the document opens successfully (unlikely), you can work with it here.
                Console.WriteLine($"Pages: {doc.Pages.Count}");
            }
        }
        catch (InvalidPasswordException ex)
        {
            // Handle the specific case where a password is required.
            Console.WriteLine($"Cannot open PDF: password required. Details: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors.
            Console.Error.WriteLine($"Error opening PDF: {ex.Message}");
        }
    }
}