using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";      // source PDF with form fields
        const string outputJsonPath = "textFields.json"; // JSON file to write the names

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Bind the PDF to the Form facade
            using (Form form = new Form(inputPdfPath))
            {
                // Collect only the names of fields whose type is Text
                List<string> textFieldNames = new List<string>();
                foreach (string fieldName in form.FieldNames)
                {
                    // Get the field type – this returns a FieldType enum, not a string
                    var fieldType = form.GetFieldType(fieldName);
                    if (fieldType == FieldType.Text)
                    {
                        textFieldNames.Add(fieldName);
                    }
                }

                // Serialize the list of text field names as a JSON array
                using (FileStream jsonStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
                {
                    JsonSerializer.Serialize(jsonStream, textFieldNames);
                }
            }

            Console.WriteLine($"Exported {outputJsonPath} containing {new FileInfo(outputJsonPath).Length} bytes.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
