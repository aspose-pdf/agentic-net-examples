using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "form_template.pdf";   // PDF with fillable form fields
        const string outputPdf  = "form_filled_encrypted.pdf";
        const string userPwd    = "user123";
        const string ownerPwd   = "owner123";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (using the standard Document constructor)
            using (Document doc = new Document(inputPdf))
            {
                // ----- Fill form fields -----
                // Access the Form collection and set values by field name.
                // Adjust field names to match those in your PDF.
                if (doc.Form != null && doc.Form.Count > 0)
                {
                    // Example: set a text field named "Name"
                    if (doc.Form["Name"] is TextBoxField nameField)
                        nameField.Value = "John Doe";

                    // Example: set a text field named "Email"
                    if (doc.Form["Email"] is TextBoxField emailField)
                        emailField.Value = "john.doe@example.com";

                    // Example: set a checkbox named "Subscribe"
                    if (doc.Form["Subscribe"] is CheckboxField subscribeField)
                        subscribeField.Checked = true; // or subscribeField.Value = "Yes" depending on PDF definition

                    // Add more field assignments as needed, casting to the appropriate field type.
                }

                // ----- Encrypt the document -----
                // Define permissions (allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Use CryptoAlgorithm.AESx256 as recommended
                doc.Encrypt(userPwd, ownerPwd, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Form filled and encrypted PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
