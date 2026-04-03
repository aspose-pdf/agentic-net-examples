using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // PDF containing the password field
        const string outputPdf = "output.pdf";     // PDF after setting the password
        const string fieldName = "PasswordField"; // Exact name of the password box field
        const string password  = "Secret123";     // Password to set

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // NOTE: FormEditor does NOT provide a SetFieldPassword method.
            // The password for a PasswordBoxField is set by assigning its Value.
            // We use the core Document API to locate the field, set the value,
            // and then (optionally) make the field read‑only via FormEditor.
            // -----------------------------------------------------------------

            // Locate the password field in the form collection
            if (doc.Form[fieldName] is PasswordBoxField pwdField)
            {
                // Set the password value
                pwdField.Value = password;

                // OPTIONAL: make the field read‑only so the user cannot change it
                using (FormEditor editor = new FormEditor(doc))
                {
                    editor.SetFieldAttribute(fieldName, PropertyFlag.ReadOnly);
                    // Save changes through the editor (editor.Save writes the underlying document)
                    editor.Save();
                }

                // Save the updated PDF
                doc.Save(outputPdf);
                Console.WriteLine($"Password field '{fieldName}' set and saved to '{outputPdf}'.");
            }
            else
            {
                Console.Error.WriteLine($"Password field '{fieldName}' not found in the document.");
            }
        }
    }
}