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

        // List to hold the checked state of each checkbox field
        List<bool> checkboxStates = new List<bool>();

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains AcroForm fields
            if (doc.Form != null && doc.Form.Fields != null)
            {
                foreach (var field in doc.Form.Fields)
                {
                    // Process only checkbox fields
                    if (field is CheckboxField checkBox)
                    {
                        // Add the checked state (true = checked, false = unchecked)
                        checkboxStates.Add(checkBox.Checked);
                    }
                }
            }
        }

        // Serialize the boolean list to JSON and output it
        string json = JsonSerializer.Serialize(checkboxStates);
        Console.WriteLine(json);
    }
}