using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // Ensure the Form facade namespace is available
using Aspose.Pdf.Facades; // FieldType enum resides here

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

        // Initialize the Form facade for the PDF
        using (Form form = new Form(inputPdf))
        {
            // Retrieve all field names in the document
            string[] allFieldNames = form.FieldNames;
            List<string> textFieldNames = new List<string>();

            // Filter only fields whose type is Text
            foreach (string fieldName in allFieldNames)
            {
                // GetFieldType returns a FieldType enum, not a string
                var fieldType = form.GetFieldType(fieldName);
                if (fieldType == FieldType.Text)
                {
                    textFieldNames.Add(fieldName);
                }
            }

            // Serialize the list of text field names to a JSON array
            string json = JsonSerializer.Serialize(textFieldNames, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON output to a file
            File.WriteAllText(outputJson, json);
        }

        Console.WriteLine($"Text field names exported to '{outputJson}'.");
    }
}
