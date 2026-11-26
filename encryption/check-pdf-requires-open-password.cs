using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "encrypted.pdf";
        const string jsonPath = "requiresPassword.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        bool requiresPassword = false;

        try
        {
            // Try to open the PDF without providing a password.
            // If this succeeds, the document does not require an open password.
            using (Document doc = new Document(pdfPath))
            {
                requiresPassword = false;
            }
        }
        catch (InvalidPasswordException)
        {
            // Aspose.Pdf throws InvalidPasswordException when a password is required.
            requiresPassword = true;
        }
        catch (Exception ex)
        {
            // Any other exception is unexpected; report and exit.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
            return;
        }

        // Serialize the result to JSON: { "requiresPassword": true/false }
        string json = JsonSerializer.Serialize(new { requiresPassword });

        try
        {
            File.WriteAllText(jsonPath, json);
            Console.WriteLine($"Result written to {jsonPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write JSON file: {ex.Message}");
        }
    }
}