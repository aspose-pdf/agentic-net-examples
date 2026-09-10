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
        const string fieldName = "Password"; // name of the password field in the PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the password field by name
            PasswordBoxField pwdField = doc.Form[fieldName] as PasswordBoxField;

            if (pwdField != null)
            {
                // Set maximum length to 20 characters
                pwdField.MaxLen = 20;
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