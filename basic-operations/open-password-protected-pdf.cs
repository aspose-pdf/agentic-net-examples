using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "protected.pdf";

        // Verify the file exists before attempting to open it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Attempt to open the PDF without providing a password.
            // If the document is encrypted, this will throw InvalidPasswordException.
            using (Document doc = new Document(inputPath))
            {
                // If the document opens successfully, you can work with it here.
                Console.WriteLine($"Document opened. Page count: {doc.Pages.Count}");
            }
        }
        catch (InvalidPasswordException ex)
        {
            // Handle the case where the PDF requires a password.
            Console.Error.WriteLine("Failed to open the PDF: it is password protected.");
            Console.Error.WriteLine($"Exception message: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors.
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}