using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF with form fields
        const string outputJson = "textFields.json"; // JSON file to write the field names

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF form using the Facades Form class
        Form form = new Form(inputPdf);

        // Collect only text field names
        var textFieldNames = new List<string>();
        foreach (string fieldName in form.FieldNames)
        {
            // Get the field type; GetFieldType returns a FieldType enum
            var fieldType = form.GetFieldType(fieldName);
            if (fieldType == FieldType.Text)
            {
                textFieldNames.Add(fieldName);
            }
        }

        // Serialize the list of names to a JSON array
        string json = JsonSerializer.Serialize(textFieldNames, new JsonSerializerOptions { WriteIndented = true });

        // Write the JSON to the output file (simpler API)
        File.WriteAllText(outputJson, json);

        Console.WriteLine($"Exported {textFieldNames.Count} text field name(s) to '{outputJson}'.");
    }
}
