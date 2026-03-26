using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // needed for form field creation helpers
using Aspose.Pdf.Forms; // <-- added for TextBoxField
using Newtonsoft.Json;

namespace ExportFormDataExample
{
    // Represents a single form field exported to JSON
    public class FieldItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class Program
    {
        private const string InputPdfPath = "form.pdf";
        private const string JsonPath = "formdata.json";

        public static void Main()
        {
            // -----------------------------------------------------------------
            // 1️⃣ Ensure a source PDF exists. If it does not, create a minimal PDF
            //    with a single text box form field so the example can run without
            //    external resources.
            // -----------------------------------------------------------------
            EnsureSamplePdfExists(InputPdfPath);

            // -----------------------------------------------------------------
            // 2️⃣ Export all form fields to a JSON file using the Aspose.Pdf API.
            // -----------------------------------------------------------------
            using (Document document = new Document(InputPdfPath))
            {
                using (FileStream jsonStream = new FileStream(JsonPath, FileMode.Create, FileAccess.Write))
                {
                    // The ExportToJson method writes a JSON array of field objects.
                    document.Form.ExportToJson(jsonStream);
                }
            }

            // -----------------------------------------------------------------
            // 3️⃣ Read the JSON content back into a string.
            // -----------------------------------------------------------------
            string jsonContent = File.ReadAllText(JsonPath);

            // -----------------------------------------------------------------
            // 4️⃣ Deserialize the JSON into a typed C# list using Json.NET.
            // -----------------------------------------------------------------
            List<FieldItem> fields = JsonConvert.DeserializeObject<List<FieldItem>>(jsonContent);

            // -----------------------------------------------------------------
            // 5️⃣ Simple verification output.
            // -----------------------------------------------------------------
            if (fields != null && fields.Count > 0)
            {
                Console.WriteLine($"Deserialized {fields.Count} form fields from JSON.");
                foreach (FieldItem field in fields)
                {
                    Console.WriteLine($"Field: {field.Name}, Value: {field.Value}");
                }
            }
            else
            {
                Console.WriteLine("No form data was deserialized.");
            }
        }

        /// <summary>
        /// Creates a very small PDF containing a single text box form field if the
        /// file specified by <paramref name="path"/> does not already exist.
        /// </summary>
        private static void EnsureSamplePdfExists(string path)
        {
            if (File.Exists(path))
                return;

            // Create a new PDF document.
            using (Document doc = new Document())
            {
                // Add a page.
                Page page = doc.Pages.Add();

                // Define a rectangle for the text box field (left, bottom, right, top).
                var rect = new Rectangle(100, 700, 300, 720);

                // Create a TextBoxField and set its properties.
                TextBoxField txtField = new TextBoxField(page, rect)
                {
                    PartialName = "SampleField",
                    Value = "SampleValue"
                };

                // Add the field to the document's form collection.
                doc.Form.Add(txtField);

                // Save the PDF so the later ExportToJson call has something to read.
                doc.Save(path);
            }
        }
    }
}
