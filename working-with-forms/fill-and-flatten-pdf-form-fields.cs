using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "form_template.pdf";
        const string outputPath = "filled_flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF containing form fields inside a using block for deterministic disposal
        using (Document pdf = new Document(inputPath))
        {
            // Example: fill a text box field named "FullName"
            if (pdf.Form["FullName"] is TextBoxField nameField)
                nameField.Value = "John Doe";

            // Example: fill a checkbox field named "Subscribe"
            if (pdf.Form["Subscribe"] is CheckboxField checkField)
                checkField.Checked = true;

            // Example: fill a radio button group named "Gender".
            // The group is represented by RadioButtonField, not RadioButtonOptionField.
            if (pdf.Form["Gender"] is RadioButtonField genderField)
            {
                // The Selected property is 1‑based. Adjust the index to the option you need.
                // Here we select the first option (e.g., "Male").
                genderField.Selected = 1;
            }

            // After all fields are populated, flatten the form so fields become static content
            pdf.Form.Flatten();

            // Save the resulting PDF; extension alone determines format (PDF)
            pdf.Save(outputPath);
        }

        Console.WriteLine($"Form filled and flattened PDF saved to '{outputPath}'.");
    }
}
