using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // PDF containing the password field
        const string outputPath = "output.pdf";     // Resulting PDF with password set
        const string fieldName  = "PasswordField"; // Exact name of the password box field
        const string newPassword = "Secret123";    // Password to assign to the field

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the password box field from the form collection
            PasswordBoxField pwdField = doc.Form[fieldName] as PasswordBoxField;

            if (pwdField == null)
            {
                Console.Error.WriteLine($"Password field '{fieldName}' not found.");
            }
            else
            {
                // Set the password value
                pwdField.Value = newPassword;

                // Optionally make the field read‑only to prevent further editing
                pwdField.ReadOnly = true;

                Console.WriteLine($"Password field '{fieldName}' updated.");
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}