using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string email      = "user@example.com";

        // Ensure the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Validate the email address contains an '@' character
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
        {
            Console.Error.WriteLine("Invalid email address. PDF will not be saved.");
            return;
        }

        try
        {
            // Document lifecycle must be wrapped in a using block (rule: document-disposal-with-using)
            using (Document doc = new Document(inputPath))
            {
                // Example usage of the validated email (e.g., store it in metadata)
                doc.Info.Author = email;

                // Save the PDF after successful validation
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}