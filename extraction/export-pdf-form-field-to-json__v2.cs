using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fieldName = "MyFormField";

        // Ensure the source PDF exists and contains the required form field
        if (!File.Exists(pdfPath))
        {
            using (var seed = new Document())
            {
                var page = seed.Pages.Add();

                // Create a simple text box form field with a sample value
                var textBox = new TextBoxField(page, new Rectangle(100, 600, 200, 20))
                {
                    PartialName = fieldName,
                    Value = "Sample value"
                };
                seed.Form.Add(textBox);
                seed.Save(pdfPath);
            }
        }

        // Load the PDF document
        using (var doc = new Document(pdfPath))
        {
            // Retrieve the field; may be null if it does not exist
            var field = doc.Form[fieldName] as Field;

            // Extract the field value safely
            string fieldValue = field?.Value?.ToString() ?? string.Empty;

            // Prepare an anonymous object for JSON serialization
            var jsonObject = new
            {
                Field = fieldName,
                Value = fieldValue
            };

            // Serialize the object into a MemoryStream and output the JSON string
            using (var ms = new MemoryStream())
            {
                JsonSerializer.Serialize(ms, jsonObject);
                ms.Position = 0;
                using (var reader = new StreamReader(ms))
                {
                    string json = reader.ReadToEnd();
                    Console.WriteLine(json);
                }
            }
        }
    }
}
