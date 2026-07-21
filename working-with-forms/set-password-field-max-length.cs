using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "Password"; // replace with actual field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the password field by name and set its maximum length
            if (doc.Form[fieldName] is PasswordBoxField passwordField)
            {
                passwordField.MaxLen = 20; // enforce max 20 characters
            }
            else
            {
                Console.Error.WriteLine($"Password field '{fieldName}' not found.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Password field max length set to 20. Saved to '{outputPath}'.");
    }
}