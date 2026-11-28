using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // DTO to hold field name and its border thickness. Initialise Name to avoid CS8618 warning.
    public class FieldBorderInfo
    {
        public string Name { get; set; } = string.Empty;
        public double BorderThickness { get; set; }
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "borders.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document actually contains a form
            if (doc.Form == null || doc.Form.Fields == null)
            {
                Console.Error.WriteLine("The PDF does not contain any form fields.");
                return;
            }

            var borderInfoList = new List<FieldBorderInfo>();

            // Iterate over each form field and capture its border thickness
            foreach (Field field in doc.Form.Fields)
            {
                // Some field types may not expose a Border; treat missing as thickness 0
                double thickness = 0;
                if (field.Border != null)
                {
                    // Border.Width is a double representing the thickness
                    thickness = field.Border.Width;
                }

                borderInfoList.Add(new FieldBorderInfo
                {
                    Name = field.Name ?? field.FullName ?? string.Empty,
                    BorderThickness = thickness
                });
            }

            // Serialize the list to JSON with indentation for readability
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(borderInfoList, jsonOptions);

            // Write the JSON to the output file
            File.WriteAllText(outputJson, json);
        }

        Console.WriteLine($"Border thickness data exported to '{outputJson}'.");
    }
}
