using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "selected_fields.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // List of form field names that should be exported.
        List<string> selectedFieldNames = new List<string>
        {
            "FirstName",
            "LastName",
            "Email"
        };

        using (Document pdfDocument = new Document(inputPdfPath))
        {
            using (FileStream outputStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(outputStream))
            {
                writer.WriteLine("{");

                for (int i = 0; i < selectedFieldNames.Count; i++)
                {
                    string fieldName = selectedFieldNames[i];
                    // Retrieve the field by name. Use the Form indexer which accepts a string key.
                    Field field = pdfDocument.Form[fieldName] as Field;
                    if (field == null)
                    {
                        // Field not found – skip.
                        continue;
                    }

                    using (MemoryStream tempStream = new MemoryStream())
                    {
                        // Export the single field value to JSON.
                        field.ExportValueToJson(tempStream, true);
                        tempStream.Position = 0;
                        using (StreamReader reader = new StreamReader(tempStream))
                        {
                            string fieldJson = reader.ReadToEnd().Trim();
                            // The exported JSON for a single field is wrapped in braces – remove them.
                            if (fieldJson.StartsWith("{") && fieldJson.EndsWith("}"))
                            {
                                fieldJson = fieldJson.Substring(1, fieldJson.Length - 2);
                            }
                            writer.Write("  " + fieldJson);
                            if (i < selectedFieldNames.Count - 1)
                            {
                                writer.WriteLine(",");
                            }
                            else
                            {
                                writer.WriteLine();
                            }
                        }
                    }
                }

                writer.WriteLine("}");
            }
        }

        Console.WriteLine($"Selected form fields exported to '{outputJsonPath}'.");
    }
}
