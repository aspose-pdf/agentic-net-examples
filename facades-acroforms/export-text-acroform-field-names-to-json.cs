using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;               // Facade namespace for Form operations
using Aspose.Pdf.Forms;                 // For FieldType enum

// Alias the Facade Form class to avoid ambiguity with Aspose.Pdf.Forms.Form
using FacadeForm = Aspose.Pdf.Facades.Form;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "text_fields.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Use the Form facade to work with PDF form fields.
        using (FacadeForm form = new FacadeForm())
        {
            form.BindPdf(inputPdfPath);

            // Retrieve all field names.
            string[] allFieldNames = form.FieldNames;

            // Filter only text fields.
            List<string> textFieldNames = new List<string>();
            foreach (string name in allFieldNames)
            {
                if (form.GetFieldType(name) == FieldType.Text)
                {
                    textFieldNames.Add(name);
                }
            }

            // Serialize the list of text field names to a JSON array.
            string json = JsonSerializer.Serialize(textFieldNames, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON output to a file.
            File.WriteAllText(outputJsonPath, json);

            Console.WriteLine($"Exported {textFieldNames.Count} text field name(s) to '{outputJsonPath}'.");
        }
    }
}
