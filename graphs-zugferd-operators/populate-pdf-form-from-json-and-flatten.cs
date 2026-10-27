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
        const string inputPdfPath = "form_template.pdf";
        const string jsonDataPath = "data.json";
        const string outputPdfPath = "filled_flattened.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(jsonDataPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonDataPath}");
            return;
        }

        // Load JSON data into a dictionary (field name -> value)
        Dictionary<string, string>? fieldValues;
        try
        {
            string json = File.ReadAllText(jsonDataPath);
            fieldValues = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            if (fieldValues == null)
            {
                Console.Error.WriteLine("JSON deserialization returned null.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read JSON data: {ex.Message}");
            return;
        }

        // Load the PDF form, populate fields, then flatten
        try
        {
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Populate each form field that exists in the PDF
                foreach (var kvp in fieldValues)
                {
                    string fieldName = kvp.Key;
                    string fieldValue = kvp.Value ?? string.Empty;

                    // Retrieve the field from the document's form collection as a Field object
                    Field? field = pdfDoc.Form[fieldName] as Field;
                    if (field != null)
                    {
                        // Set the field value using the Field.Value property
                        field.Value = fieldValue;
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Field '{fieldName}' not found in the PDF form.");
                    }
                }

                // Flatten the form to make fields non‑editable
                pdfDoc.Flatten();

                // Save the resulting PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Form populated and flattened successfully: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
