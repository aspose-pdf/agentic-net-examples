using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "text_fields.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF into the Form facade
        using (Form form = new Form(inputPdf))
        {
            var textFieldNames = new List<string>();

            // Iterate over all field names and keep only text fields
            foreach (string fieldName in form.FieldNames)
            {
                // GetFieldType returns a FieldType enum, not a string
                FieldType fieldType = form.GetFieldType(fieldName);
                if (fieldType == FieldType.Text)
                {
                    textFieldNames.Add(fieldName);
                }
            }

            // Serialize the list of names to a JSON array
            string json = JsonSerializer.Serialize(textFieldNames, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON to the output file
            File.WriteAllText(outputJson, json);
        }

        Console.WriteLine($"Exported text field names to '{outputJson}'.");
    }
}
