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
        const string inputPdfPath = "input.pdf";          // Path to the PDF with AcroForm
        const string outputJsonPath = "checkbox_states.json"; // Output JSON file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // List to hold the boolean state of each checkbox field
            List<bool> checkboxStates = new List<bool>();

            // Iterate over all form fields; filter only CheckboxField instances
            foreach (Field field in pdfDoc.Form.Fields)
            {
                if (field is CheckboxField checkbox)
                {
                    // The Checked property returns true if the box is ticked
                    checkboxStates.Add(checkbox.Checked);
                }
            }

            // Serialize the boolean list to JSON and write to the output file
            using (FileStream jsonStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerializer.Serialize(jsonStream, checkboxStates, options);
            }

            Console.WriteLine($"Extracted {checkboxStates.Count} checkbox states to '{outputJsonPath}'.");
        }
    }
}