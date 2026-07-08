using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, fill form fields, flatten them, and save.
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form.
            if (doc.Form != null && doc.Form.Count > 0)
            {
                // Set a text field named "Name".
                if (doc.Form["Name"] is TextBoxField nameField)
                {
                    nameField.Value = "John Doe";
                }

                // Set a checkbox field named "Subscribe".
                if (doc.Form["Subscribe"] is CheckboxField subscribeField)
                {
                    subscribeField.Checked = true;
                }

                // Set a radio button group named "Gender".
                if (doc.Form["Gender"] is RadioButtonField genderField)
                {
                    // Use the Value property to set the selected option by its export value.
                    genderField.Value = "Male";
                }
            }

            // Flatten all form fields so their values become part of the page content.
            doc.Form.Flatten();

            // Save the flattened PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}
