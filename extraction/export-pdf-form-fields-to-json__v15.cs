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
        const string pdfPath = "FormH.pdf";
        const string jsonPath = "FormH_fields.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        var fields = new Dictionary<string, object>();

        // Load the PDF and enumerate form fields
        using (Document doc = new Document(pdfPath))
        {
            foreach (Field field in doc.Form)
            {
                string name = field.PartialName;
                object value = field.Value;
                fields[name] = value;
            }
        }

        // Serialize the name/value pairs to JSON
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(fields, options);
        File.WriteAllText(jsonPath, json);

        Console.WriteLine($"Exported {fields.Count} form fields to '{jsonPath}'.");
    }
}