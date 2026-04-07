using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // PDF with an email form field named "email"
        const string outputPath = "validated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Retrieve the email field by its name (adjust the name if different)
            if (!form.HasField("email"))
            {
                Console.Error.WriteLine("The document does not contain a field named 'email'.");
                return;
            }

            // The field is a text box; cast to TextBoxField to access its Value property
            TextBoxField emailField = form["email"] as TextBoxField;
            if (emailField == null)
            {
                Console.Error.WriteLine("The 'email' field is not a text box field.");
                return;
            }

            // Get the current value of the field
            string emailValue = emailField.Value?.ToString() ?? string.Empty;

            // Validate that the value contains an '@' character
            if (!emailValue.Contains("@"))
            {
                Console.Error.WriteLine("Validation failed: email address must contain '@'.");
                return; // Do not save the document
            }

            // Validation passed – save the PDF
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
        }
    }
}