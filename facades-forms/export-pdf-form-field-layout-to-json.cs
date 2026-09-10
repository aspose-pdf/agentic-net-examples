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
        const string outputJson = "fields_layout.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the Facade for the PDF form
        Form form = new Form(inputPdf);

        // Collect layout information for each field
        var fieldInfos = new List<FieldInfo>();
        foreach (string fieldName in form.FieldNames)
        {
            // Obtain the facade for the specific field
            FormFieldFacade fieldFacade = form.GetFieldFacade(fieldName);

            // Position returns an array: { llx, lly, urx, ury }
            float[] pos = fieldFacade.Position;
            if (pos != null && pos.Length == 4)
            {
                fieldInfos.Add(new FieldInfo
                {
                    Name   = fieldName,
                    Left   = pos[0],
                    Bottom = pos[1],
                    Right  = pos[2],
                    Top    = pos[3]
                });
            }
        }

        // Serialize the layout data to indented JSON
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(fieldInfos, jsonOptions);

        // Write JSON to the output file
        File.WriteAllText(outputJson, json);
        Console.WriteLine($"Exported field layout to '{outputJson}'.");
    }

    // Simple DTO for JSON output
    private class FieldInfo
    {
        public string Name   { get; set; }
        public float  Left   { get; set; }
        public float  Bottom { get; set; }
        public float  Right  { get; set; }
        public float  Top    { get; set; }
    }
}