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
        const string inputPdf = "input.pdf";
        const string outputJson = "checkbox_states.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (using the standard Document constructor)
        using (Document doc = new Document(inputPdf))
        {
            // Collect the checked state of each checkbox field
            List<bool> checkboxStates = new List<bool>();

            foreach (Field field in doc.Form.Fields)
            {
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

            // Write JSON to the output file
            File.WriteAllText(outputJson, json);
        }

        Console.WriteLine($"Checkbox states exported to '{outputJson}'.");
    }
}