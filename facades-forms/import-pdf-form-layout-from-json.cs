using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfFormLayoutImport
{
    // Represents a single field definition in the JSON layout file
    public class FieldDefinition
    {
        // The properties are required – C# 11 "required" keyword tells the compiler they must be set
        public required string Name { get; set; }
        public required string Type { get; set; }
        public int Page { get; set; }
        public float Llx { get; set; }
        public float Lly { get; set; }
        public float Urx { get; set; }
        public float Ury { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string jsonPath   = "fieldLayout.json";   // JSON file with field layout
            const string outputPath = "output.pdf";         // Resulting PDF file

            // ---------------------------------------------------------------------
            // 1️⃣ Create a sample JSON layout file (self‑contained – no external files)
            // ---------------------------------------------------------------------
            var sampleFields = new List<FieldDefinition>
            {
                new FieldDefinition
                {
                    Name = "Customer.Name",
                    Type = "Text",
                    Page = 1,
                    Llx = 100,
                    Lly = 700,
                    Urx = 300,
                    Ury = 720
                },
                new FieldDefinition
                {
                    Name = "Customer.AcceptTerms",
                    Type = "CheckBox",
                    Page = 1,
                    Llx = 100,
                    Lly = 650,
                    Urx = 115,
                    Ury = 665
                }
            };
            // Serialize the sample layout to the expected file location
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(jsonPath, JsonSerializer.Serialize(sampleFields, jsonOptions));

            // ---------------------------------------------------------------------
            // 2️⃣ Load and deserialize the JSON layout (null‑safe)
            // ---------------------------------------------------------------------
            List<FieldDefinition> fields;
            using (FileStream fs = File.OpenRead(jsonPath))
            {
                fields = JsonSerializer.Deserialize<List<FieldDefinition>>(fs) ?? new List<FieldDefinition>();
            }

            // ---------------------------------------------------------------------
            // 3️⃣ Create a new PDF document and add a blank page (required before adding fields)
            // ---------------------------------------------------------------------
            using (Document doc = new Document())
            {
                doc.Pages.Add();

                // Bind the FormEditor facade to the newly created document
                FormEditor formEditor = new FormEditor(doc);

                // -----------------------------------------------------------------
                // 4️⃣ Add each field according to the layout information
                // -----------------------------------------------------------------
                foreach (var f in fields)
                {
                    // Convert the string representation of the field type to the enum value
                    // Enum.Parse can throw if the value is invalid – guard with TryParse for safety
                    if (!Enum.TryParse<FieldType>(f.Type, true, out var fieldType))
                    {
                        Console.WriteLine($"Unsupported field type '{f.Type}' for field '{f.Name}'. Skipping.");
                        continue;
                    }

                    // Add the field at the specified page and rectangle
                    formEditor.AddField(
                        fieldType,
                        f.Name,
                        f.Page,
                        f.Llx,
                        f.Lly,
                        f.Urx,
                        f.Ury);
                }

                // Save the populated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with imported field layout saved to '{outputPath}'.");
        }
    }
}
