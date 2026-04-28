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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the PDF form using Aspose.Pdf.Facades.Form
        using (Form form = new Form(inputPdf))
        {
            // Retrieve all field names in the document
            string[] allFieldNames = form.FieldNames;

            // Collect only text fields
            List<string> textFieldNames = new List<string>();
            foreach (string fieldName in allFieldNames)
            {
                // Get the type of the field as an enum (FieldType)
                var fieldType = form.GetFieldType(fieldName);

                // Compare with the FieldType enum values (case‑insensitive comparison not needed for enums)
                if (fieldType == FieldType.Text)
                {
                    textFieldNames.Add(fieldName);
                }
            }

            // Export the list of text field names as a JSON array
            using (FileStream fs = new FileStream(outputJson, FileMode.Create, FileAccess.Write))
            {
                JsonSerializer.Serialize(fs, textFieldNames);
            }

            Console.WriteLine($"Exported {textFieldNames.Count} text field name(s) to '{outputJson}'.");
        }
    }
}
