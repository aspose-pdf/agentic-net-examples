using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";   // PDF containing the original form data
        const string targetPdfPath = "target.pdf";   // PDF that will receive the transformed data

        // ------------------------------------------------------------
        // 1. Create a source PDF with a sample form field ("oldName")
        // ------------------------------------------------------------
        using (Document sourceSeed = new Document())
        {
            sourceSeed.Pages.Add();
            // Add a textbox field named "oldName"
            TextBoxField oldField = new TextBoxField(sourceSeed.Pages[1], new Rectangle(100, 700, 300, 730))
            {
                PartialName = "oldName",
                Value = "Sample value"
            };
            sourceSeed.Form.Add(oldField, 1);
            sourceSeed.Save(sourcePdfPath);
        }

        // ------------------------------------------------------------
        // 2. Create a target PDF that contains a field with the new name
        // ------------------------------------------------------------
        using (Document targetSeed = new Document())
        {
            targetSeed.Pages.Add();
            // Add a textbox field named "newName" (empty value – will be filled by import)
            TextBoxField newField = new TextBoxField(targetSeed.Pages[1], new Rectangle(100, 700, 300, 730))
            {
                PartialName = "newName"
            };
            targetSeed.Form.Add(newField, 1);
            targetSeed.Save(targetPdfPath);
        }

        // ------------------------------------------------------------
        // 3. Load the source PDF, export its form data to JSON, transform, and import into target PDF
        // ------------------------------------------------------------
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // Export the form fields to a JSON stream (in memory)
            using (MemoryStream exportStream = new MemoryStream())
            {
                sourceDoc.Form.ExportToJson(exportStream);
                exportStream.Position = 0; // Reset for reading

                // Read the JSON text (skip possible BOM)
                string jsonText = new StreamReader(exportStream, Encoding.UTF8).ReadToEnd();
                if (jsonText.Length > 0 && jsonText[0] == '\uFEFF')
                {
                    jsonText = jsonText.Substring(1);
                }

                // The JSON produced by ExportToJson is an array, not an object.
                // Parse it as a JArray, rename the field, then write it back.
                JArray fieldsArray = JArray.Parse(jsonText);

                // ----- Example transformation -----
                // Rename field "oldName" to "newName"
                foreach (JObject field in fieldsArray)
                {
                    if (field["Name"]?.ToString() == "oldName")
                    {
                        field["Name"] = "newName";
                    }
                }
                // ----- End of example transformation -----

                // Write the transformed JSON back to a new memory stream **without** BOM
                using (MemoryStream importStream = new MemoryStream())
                using (StreamWriter writer = new StreamWriter(importStream, new UTF8Encoding(false)))
                {
                    writer.Write(fieldsArray.ToString(Formatting.None));
                    writer.Flush();
                    importStream.Position = 0; // Reset for reading

                    // Load the target PDF (could be a different document)
                    using (Document targetDoc = new Document(targetPdfPath))
                    {
                        // Import the transformed form data into the target PDF
                        targetDoc.Form.ImportFromJson(importStream);

                        // Save the updated PDF
                        targetDoc.Save(targetPdfPath);
                    }
                }
            }
        }
    }
}
