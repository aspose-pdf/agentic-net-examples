using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Newtonsoft.Json;

namespace AsposePdfFormJsonExample
{
    // Represents a single form field exported as JSON.
    public class FormField
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string pdfPath = "input_form.pdf";

            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                return;
            }

            // Export form fields to JSON using Aspose.Pdf.Facades.Form.
            // The Form class can be instantiated directly with the PDF file path.
            Form form = new Form(pdfPath);

            // Use a memory stream to capture the JSON output.
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // ExportJson writes the fields as indented JSON by default.
                form.ExportJson(jsonStream, indented: true);

                // Reset the stream position before reading.
                jsonStream.Position = 0;

                // Read the JSON text.
                string jsonText;
                using (StreamReader reader = new StreamReader(jsonStream))
                {
                    jsonText = reader.ReadToEnd();
                }

                // Deserialize the JSON into a strongly‑typed list of FormField objects.
                // The JSON format produced by ExportJson is an array of objects with
                // "Name" and "Value" properties, which matches the FormField class.
                List<FormField> fields = JsonConvert.DeserializeObject<List<FormField>>(jsonText);

                // Example processing: display each field name and its value.
                Console.WriteLine("Exported form fields:");
                foreach (FormField field in fields)
                {
                    Console.WriteLine($"  {field.Name}: {field.Value}");
                }
            }

            // No explicit save operation is required here because we only exported data.
        }
    }
}