using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "checkbox_states.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Prepare a list to hold the boolean states of all checkbox fields
            List<bool> checkboxStates = new List<bool>();

            // Iterate over all form fields in the AcroForm. The Form collection implements IEnumerable<Field>.
            foreach (Field field in doc.Form)
            {
                // Check if the current field is a CheckboxField
                if (field is CheckboxField checkbox)
                {
                    // The Checked property indicates whether the box is selected
                    checkboxStates.Add(checkbox.Checked);
                }
            }

            // Serialize the boolean list to JSON
            string json = JsonSerializer.Serialize(checkboxStates, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON output to a file
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Checkbox states saved to '{outputJsonPath}'.");
        }
    }
}