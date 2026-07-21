using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";      // source PDF with form fields
        const string outputJson = "textFields.json"; // JSON file to write the names to

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF form using the Facades Form class
        using (Form form = new Form(inputPdf))
        {
            // Collect only the names of fields whose type is Text
            List<string> textFieldNames = new List<string>();

            foreach (string fieldName in form.FieldNames)
            {
                // GetFieldType returns a FieldType enum (e.g., FieldType.Text)
                FieldType fieldType = form.GetFieldType(fieldName);
                if (fieldType == FieldType.Text)
                {
                    textFieldNames.Add(fieldName);
                }
            }

            // Serialize the list of names as a JSON array
            string json = JsonSerializer.Serialize(textFieldNames, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON to the output file
            File.WriteAllText(outputJson, json);
        }

        Console.WriteLine($"Exported {outputJson}");
    }
}
