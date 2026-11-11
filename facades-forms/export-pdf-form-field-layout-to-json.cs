using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExportFormFieldsToJson
{
    static void Main()
    {
        // Input PDF containing form fields
        const string inputPdf = "form.pdf";
        // Output JSON file with field layout information
        const string outputJson = "form_fields.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPdf);

        // Prepare a list to hold field layout data
        var fields = new List<object>();

        // Iterate over all form fields present in the document using the correct Field type
        foreach (Field field in doc.Form.Fields)
        {
            // The field's name (PartialName) and its rectangle (coordinates)
            string name = field.PartialName;
            Aspose.Pdf.Rectangle rect = field.Rect;

            double width = rect.URX - rect.LLX;
            double height = rect.URY - rect.LLY;

            fields.Add(new
            {
                Name = name,
                X = rect.LLX,
                Y = rect.LLY,
                Width = width,
                Height = height
            });
        }

        // Serialize the list to formatted JSON
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(fields, jsonOptions);

        // Write JSON to the output file
        File.WriteAllText(outputJson, json);

        Console.WriteLine($"Form field layout exported to '{outputJson}'.");
    }
}
