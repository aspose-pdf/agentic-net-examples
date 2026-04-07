using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF containing the password field
        const string outputPdf = "output.pdf";  // PDF after setting the password
        const string fieldName = "PasswordField"; // exact name of the password box field
        const string password  = "Secret123"; // password to set

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor works with the Facades API; it loads the PDF and provides access to the underlying Document.
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF file
            formEditor.BindPdf(inputPdf);

            // Retrieve the underlying Document object
            Document doc = formEditor.Document;

            // Locate the field by its name and cast to PasswordBoxField
            PasswordBoxField pwdField = doc.Form[fieldName] as PasswordBoxField;
            if (pwdField == null)
            {
                Console.Error.WriteLine($"Password field '{fieldName}' not found or is not a PasswordBoxField.");
                return;
            }

            // Set the password value directly on the field
            pwdField.Value = password;

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Password field '{fieldName}' updated and saved to '{outputPdf}'.");
    }
}