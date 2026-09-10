using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

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

        // Open the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a form object
            Form form = doc.Form;

            // Define the rectangle for the password field (lower‑left x, lower‑y, upper‑right x, upper‑y)
            Rectangle rect = new Rectangle(100, 500, 300, 530);

            // Create a text box field (used as a password field) on the first page
            TextBoxField pwdField = new TextBoxField(doc.Pages[1], rect);
            pwdField.PartialName = fieldName;   // set the field name
            pwdField.Value = password;          // set the password value
            pwdField.ReadOnly = true;           // make the field read‑only to enforce security
            pwdField.Color = Color.LightGray;   // optional visual styling
            pwdField.MaxLen = password.Length;  // optional length restriction

            // Add the field to the document's form
            form.Add(pwdField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Password field added and saved to '{outputPath}'.");
    }
}
