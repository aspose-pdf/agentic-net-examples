using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF containing the checkbox field
        const string inputPath = "input.pdf";
        // Output PDF with the checkbox updated
        const string outputPath = "output.pdf";
        // Name of the checkbox field to modify
        const string fieldName = "myCheckBox";

        // Example input data: set the checkbox to checked (true) or unchecked (false)
        bool setChecked = true; // replace with actual input logic as needed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document disposal with using)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the checkbox field by its name
            CheckboxField checkbox = doc.Form[fieldName] as CheckboxField;

            if (checkbox != null)
            {
                // Set the checkbox state based on input data
                checkbox.Checked = setChecked;
                // Alternatively, you can set the Value property to an allowed state:
                // checkbox.Value = setChecked ? checkbox.AllowedStates[0] : "Off";
            }
            else
            {
                Console.Error.WriteLine($"Checkbox field '{fieldName}' not found.");
            }

            // Save the modified PDF (using rule: document disposal with using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}