using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Fill form fields if they exist
            if (doc.Form != null && doc.Form.Count > 0)
            {
                // Text field "Name"
                if (doc.Form["Name"] is TextBoxField nameField)
                {
                    nameField.Value = "John Doe";
                }

                // Checkbox field "Agree"
                if (doc.Form["Agree"] is CheckboxField agreeField)
                {
                    agreeField.Checked = true;
                }

                // Combo box field "Country"
                if (doc.Form["Country"] is ComboBoxField countryField)
                {
                    // The selected value is set via the Value property.
                    // The list of possible items is stored in countryField.Options.
                    countryField.Value = "USA";
                }
            }

            // Flatten all form fields – they become part of the page content and are no longer editable
            doc.Flatten();

            // Save the flattened PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}