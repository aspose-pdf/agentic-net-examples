using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // PDF containing the password field
        const string outputPdf = "output.pdf";     // PDF after setting the password
        const string fieldName = "PasswordField"; // Exact name of the password box field
        const string newPassword = "Secret123";   // Password to assign to the field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (no Facades are used)
        using (Document doc = new Document(inputPdf))
        {
            // Access the form object from the document
            Form form = doc.Form;

            // Retrieve the field by name and cast it to PasswordBoxField
            PasswordBoxField pwdField = form[fieldName] as PasswordBoxField;
            if (pwdField == null)
            {
                Console.Error.WriteLine($"Password field '{fieldName}' not found or is not a PasswordBoxField.");
                return;
            }

            // Set the password value
            pwdField.Value = newPassword;

            // Optionally make the field read‑only so the user cannot edit it
            pwdField.ReadOnly = true;

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Password field '{fieldName}' updated and saved to '{outputPdf}'.");
    }
}