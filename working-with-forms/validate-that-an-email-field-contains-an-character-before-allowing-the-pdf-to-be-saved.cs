using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Check for the presence of the email field (assumed name: "email")
            if (!doc.Form.HasField("email"))
            {
                Console.Error.WriteLine("Email field not found. PDF not saved.");
                return;
            }

            // Retrieve the field and its current value
            TextBoxField emailField = (TextBoxField)doc.Form["email"];
            string emailValue = emailField.Value?.ToString() ?? string.Empty;

            // Validate that the value contains an '@' character
            if (!emailValue.Contains("@"))
            {
                Console.Error.WriteLine("Invalid email address: missing '@' character. PDF not saved.");
                return;
            }

            // Save the PDF (PDF format does not require explicit SaveOptions)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
    }
}