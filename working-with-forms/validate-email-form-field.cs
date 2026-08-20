using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF with a form field named "email"
        const string outputPath = "validated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field named "email" and cast it to a Field
            Field emailField = doc.Form["email"] as Field;
            if (emailField == null)
            {
                Console.Error.WriteLine("Email field not found or is not a form field.");
                return;
            }

            // Get the current value of the field (may be null)
            string emailValue = emailField.Value?.ToString() ?? string.Empty;

            // Validate that the value contains an '@' character
            if (!emailValue.Contains("@"))
            {
                Console.Error.WriteLine("Validation failed: email address must contain '@'.");
                return;
            }

            // Validation passed – save the document
            doc.Save(outputPath);
            Console.WriteLine($"Document saved successfully to '{outputPath}'.");
        }
    }
}