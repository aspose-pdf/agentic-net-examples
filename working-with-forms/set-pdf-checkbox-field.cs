using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF with a checkbox field
        const string inputPdfPath  = "input.pdf";
        // Output PDF after setting the checkbox
        const string outputPdfPath = "output_checked.pdf";
        // Example input data: set the checkbox to checked (true) or unchecked (false)
        bool setChecked = true; // this could come from user input, arguments, etc.

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Locate the checkbox field.
            // Option 1: by field name (replace "myCheckbox" with the actual field name)
            // CheckboxField checkbox = form["myCheckbox"] as CheckboxField;

            // Option 2: by index (first field assumed to be a checkbox)
            CheckboxField checkbox = form.Fields[0] as CheckboxField;

            if (checkbox == null)
            {
                Console.Error.WriteLine("Checkbox field not found in the document.");
                return;
            }

            // Set the checkbox state based on input data
            checkbox.Checked = setChecked; // true = checked, false = unchecked

            // Alternatively, you can set the Value property to "On" or "Off"
            // checkbox.Value = setChecked ? "On" : "Off";

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Checkbox state set to {(setChecked ? "checked" : "unchecked")} and saved to '{outputPdfPath}'.");
    }
}