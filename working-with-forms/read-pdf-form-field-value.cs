using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // path to the PDF containing the form
        const string fieldName = "MyField";           // name of the form field to read

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the field by name (Form indexer returns a WidgetAnnotation;
            // cast to Field to access ExportValueToJson)
            Field field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Field \"{fieldName}\" not found.");
                return;
            }

            // Export the field value to a JSON stream
            using (MemoryStream jsonStream = new MemoryStream())
            {
                field.ExportValueToJson(jsonStream, indented: true);
                jsonStream.Position = 0; // rewind for reading

                // Read the JSON text
                string json;
                using (StreamReader reader = new StreamReader(jsonStream, Encoding.UTF8))
                {
                    json = reader.ReadToEnd();
                }

                // The JSON format for a single field is:
                // { "fieldFullName": "fieldValue" }
                // Extract the value part (simple parsing without external libs)
                int colonIndex = json.IndexOf(':');
                if (colonIndex >= 0 && colonIndex + 1 < json.Length)
                {
                    // Trim surrounding whitespace, quotes and braces
                    string rawValue = json.Substring(colonIndex + 1).Trim().Trim('}', '\"');
                    Console.WriteLine($"Field \"{fieldName}\" value: {rawValue}");
                }
                else
                {
                    Console.WriteLine($"Unable to parse field value from JSON: {json}");
                }
            }
        }
    }
}