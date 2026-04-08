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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Collect the checked state of each checkbox field
            List<bool> checkboxStates = new List<bool>();

            foreach (Field field in doc.Form.Fields)
            {
                // Only process CheckboxField instances
                if (field is CheckboxField checkbox)
                {
                    // The Checked property returns true if the box is selected
                    checkboxStates.Add(checkbox.Checked);
                }
            }

            // Serialize the boolean list to JSON
            string json = JsonSerializer.Serialize(checkboxStates, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Output JSON to console (or write to a file if desired)
            Console.WriteLine(json);
        }
    }
}