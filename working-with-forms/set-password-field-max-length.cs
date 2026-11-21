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

        // Open the PDF document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the password field by its name (replace "PasswordField" with the actual field name)
            TextBoxField passwordField = doc.Form["PasswordField"] as TextBoxField;
            if (passwordField == null)
            {
                Console.Error.WriteLine("Password field not found.");
                return;
            }

            // The field is already defined as a password field in the PDF.
            // If you need to enforce the password behaviour programmatically, Aspose.PDF does not expose an IsPassword property.
            // The important part for this task is limiting the input length.

            // Set the maximum allowed length to 20 characters (use MaxLen, not MaxLength)
            passwordField.MaxLen = 20;

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Password field max length set to 20 characters. Saved to '{outputPath}'.");
    }
}
