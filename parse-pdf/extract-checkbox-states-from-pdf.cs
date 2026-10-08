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
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form
            if (doc.Form == null || doc.Form.Fields == null)
            {
                Console.WriteLine("[]"); // No form fields, output empty JSON array
                return;
            }

            List<bool> checkboxStates = new List<bool>();

            // Iterate over all form fields
            foreach (Field field in doc.Form.Fields)
            {
                // Identify checkbox fields using the fully qualified type
                if (field is CheckboxField checkBox)
                {
                    // The Checked property indicates the state of the checkbox
                    checkboxStates.Add(checkBox.Checked);
                }
            }

            // Serialize the list of booleans to JSON
            string json = JsonSerializer.Serialize(checkboxStates);
            Console.WriteLine(json);
        }
    }
}