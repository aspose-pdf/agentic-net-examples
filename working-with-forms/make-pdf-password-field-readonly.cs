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

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Core API does not expose a method to set a password on a form field.
            // As a workaround, we can make the password field read‑only to prevent editing.
            // Replace "PasswordField" with the actual fully qualified name of the field.
            const string fieldName = "PasswordField";

            if (doc.Form.HasField(fieldName))
            {
                // Cast to PasswordBoxField to access field‑specific properties.
                PasswordBoxField pwdField = doc.Form[fieldName] as PasswordBoxField;
                if (pwdField != null)
                {
                    // Make the field read‑only.
                    pwdField.ReadOnly = true;
                }
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}