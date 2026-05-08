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
        const string fieldName = "PasswordField";
        const string password = "Secret123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Retrieve the password field from the form
            PasswordBoxField pwdField = doc.Form[fieldName] as PasswordBoxField;
            if (pwdField == null)
            {
                Console.Error.WriteLine($"Password field '{fieldName}' not found.");
                return;
            }

            // Set the password value
            pwdField.Value = password;

            // Make the field read‑only to enforce security
            pwdField.ReadOnly = true;

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Password set and document saved to '{outputPath}'.");
    }
}