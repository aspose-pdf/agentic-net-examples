using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object; it contains all form fields
            Aspose.Pdf.Forms.Form form = doc.Form;

            // Prepare a dictionary to hold field names and their extracted values
            var fieldValues = new Dictionary<string, object>();

            // Iterate over all fields in the form
            foreach (Aspose.Pdf.Forms.Field field in form.Fields)
            {
                // Use the fully qualified field name as the JSON key
                string fieldName = field.FullName;

                // Determine the concrete field type and extract its value accordingly
                if (field is Aspose.Pdf.Forms.TextBoxField textBox)
                {
                    // TextBoxField stores its content as a string
                    fieldValues[fieldName] = textBox.Value;
                }
                else if (field is Aspose.Pdf.Forms.CheckboxField checkBox)
                {
                    // CheckboxField exposes a boolean indicating whether it is checked
                    fieldValues[fieldName] = checkBox.Checked;
                }
                else if (field is Aspose.Pdf.Forms.RadioButtonOptionField radioButton)
                {
                    // RadioButtonOptionField returns the selected option value as a string
                    fieldValues[fieldName] = radioButton.Value;
                }
                else if (field is Aspose.Pdf.Forms.ListBoxField listBox)
                {
                    // ListBoxField may have multiple selected items; capture them as an array
                    fieldValues[fieldName] = listBox.SelectedItems;
                }
                else if (field is Aspose.Pdf.Forms.ComboBoxField comboBox)
                {
                    // ComboBoxField returns the selected value as a string
                    fieldValues[fieldName] = comboBox.Value;
                }
                else
                {
                    // Fallback for any other field types: use the generic string representation
                    fieldValues[fieldName] = field.ToString();
                }
            }

            // Serialize the dictionary to a formatted JSON string
            string jsonOutput = JsonSerializer.Serialize(
                fieldValues,
                new JsonSerializerOptions { WriteIndented = true });

            // Output the JSON to the console (or write to a file if desired)
            Console.WriteLine(jsonOutput);
        }
    }
}