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
        const string inputPdf = "input.pdf";
        const string outputJson = "checkbox_states.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Collect the checked state of each checkbox field
            List<bool> checkboxStates = new List<bool>();

            // Iterate over form fields (Field) instead of non‑existent WidgetAnnotation
            foreach (Field field in doc.Form.Fields)
            {
                if (field is CheckboxField checkbox)
                {
                    // The Checked property returns true if the box is selected
                    checkboxStates.Add(checkbox.Checked);
                }
            }

            // Serialize the boolean list to JSON and write to file
            using (FileStream fs = new FileStream(outputJson, FileMode.Create, FileAccess.Write))
            {
                JsonSerializer.Serialize(fs, checkboxStates);
            }

            Console.WriteLine($"Extracted {checkboxStates.Count} checkbox states to '{outputJson}'.");
        }
    }
}
