using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "template.pdf";
        const string outputPath = "encrypted_filled.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF that contains form fields
            using (Document doc = new Document(inputPath))
            {
                // Example: fill a text box field named "Name"
                if (doc.Form["Name"] != null && doc.Form["Name"] is TextBoxField nameField)
                {
                    nameField.Value = "John Doe";
                }

                // Example: check a checkbox field named "Subscribe"
                if (doc.Form["Subscribe"] != null && doc.Form["Subscribe"] is CheckboxField checkField)
                {
                    checkField.Checked = true;
                }

                // Example: select an item in a combo box field named "Country"
                if (doc.Form["Country"] != null && doc.Form["Country"] is ComboBoxField comboField)
                {
                    // Set the selected value directly via the Value property.
                    // The value must match one of the existing options in the combo box.
                    comboField.Value = "USA";
                }

                // Apply encryption after the form is filled
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved and encrypted to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
