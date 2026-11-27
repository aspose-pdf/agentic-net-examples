using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputJsonPath = "form_fields.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for proper disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Prepare a list to hold field information
                var fieldsInfo = new List<object>();

                // Iterate over all form fields in the document
                foreach (Field field in pdfDoc.Form)
                {
                    // Capture basic properties: name, type, and current value
                    fieldsInfo.Add(new
                    {
                        Name  = field.FullName,
                        Type  = field.GetType().Name,
                        Value = field.Value?.ToString()
                    });
                }

                // Serialize the list to JSON with indentation
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(fieldsInfo, jsonOptions);

                // Write JSON to the output file
                File.WriteAllText(outputJsonPath, json);
                Console.WriteLine($"Form fields exported to '{outputJsonPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}