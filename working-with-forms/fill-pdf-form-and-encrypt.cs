using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // PDF with form fields
        const string outputPdf  = "filled_encrypted.pdf";
        const string userPwd    = "user123";
        const string ownerPwd   = "owner123";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: load)
            using (Document doc = new Document(inputPdf))
            {
                // Fill form fields (example field names)
                // TextBoxField
                if (doc.Form["Name"] is TextBoxField nameField)
                    nameField.Value = "John Doe";

                // CheckboxField
                if (doc.Form["Subscribe"] is CheckboxField checkField)
                    checkField.Checked = true;

                // ComboBoxField – select the third option (index 2) using Options collection
                if (doc.Form["Country"] is ComboBoxField comboField)
                {
                    if (comboField.Options != null && comboField.Options.Count > 2)
                    {
                        // Set the value of the field to the value of the third option
                        comboField.Value = comboField.Options[2].Value;
                    }
                }

                // Set permissions (allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt the document using AES-256 (encryption rule)
                doc.Encrypt(userPwd, ownerPwd, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF (lifecycle: save)
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
