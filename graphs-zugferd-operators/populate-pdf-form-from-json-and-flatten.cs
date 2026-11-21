using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // required for Form field types

class Program
{
    static void Main()
    {
        const string pdfPath = "input_form.pdf";
        const string jsonPath = "data.json";
        const string outputPdf = "filled_flattened.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonPath}");
            return;
        }

        // Load JSON key/value pairs (field name -> value)
        Dictionary<string, string> fieldData;
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            fieldData = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read JSON data: {ex.Message}");
            return;
        }

        // Open the PDF, populate fields, then flatten the form
        try
        {
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Populate each form field that exists in the PDF
                foreach (var kvp in fieldData)
                {
                    string fieldName = kvp.Key;
                    string fieldValue = kvp.Value;

                    // Verify the field exists
                    if (pdfDoc.Form.HasField(fieldName))
                    {
                        // Retrieve the field as a Form.Field (not WidgetAnnotation)
                        Field field = pdfDoc.Form[fieldName] as Field;
                        if (field != null)
                        {
                            // Set the value – the Value property is defined on Aspose.Pdf.Forms.Field
                            field.Value = fieldValue;
                        }
                        else
                        {
                            Console.WriteLine($"Warning: Field '{fieldName}' could not be cast to a Form.Field.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Field '{fieldName}' not found in the PDF.");
                    }
                }

                // Flatten the form to make fields non‑editable
                pdfDoc.Form.Flatten();

                // Save the resulting PDF
                pdfDoc.Save(outputPdf);
            }

            Console.WriteLine($"Form fields populated and flattened. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
