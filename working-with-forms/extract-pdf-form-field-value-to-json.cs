using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string fieldName = "MyField"; // replace with the actual field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to Field.
            Field field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.WriteLine($"Field '{fieldName}' not found or is not a form field.");
                return;
            }

            // Export the field value to a JSON stream
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // Export only the selected field's value to JSON
                field.ExportValueToJson(jsonStream, true);
                jsonStream.Position = 0; // reset stream position for reading

                using (StreamReader reader = new StreamReader(jsonStream))
                {
                    string json = reader.ReadToEnd();
                    Console.WriteLine($"Field \"{fieldName}\" value (JSON): {json}");
                }
            }
        }
    }
}
