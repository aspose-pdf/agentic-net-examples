using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // <-- required for Form fields

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "form.pdf";
        const string outputPdfPath = "filled_form.pdf";
        const string jsonPath      = "data.json";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Load JSON mapping of field names to values
        Dictionary<string, string> fieldValues;
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            fieldValues = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent) ?? new Dictionary<string, string>();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read or parse JSON: {ex.Message}");
            return;
        }

        // Load the PDF document
        Document pdfDocument;
        try
        {
            pdfDocument = new Document(inputPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load PDF: {ex.Message}");
            return;
        }

        // Fill form fields using the values from the JSON dictionary
        foreach (var kvp in fieldValues)
        {
            // Retrieve the form field by its partial name (the key from JSON)
            // Document.Form indexer returns a WidgetAnnotation; cast to Aspose.Pdf.Forms.Field to access the Value property.
            Field formField = pdfDocument.Form[kvp.Key] as Field;
            if (formField != null)
            {
                // Most field types expose a Value property that accepts a string.
                // For specific types (e.g., CheckBoxField) you may want to set the appropriate property,
                // but assigning to Value works for the majority of cases.
                formField.Value = kvp.Value;
            }
            else
            {
                Console.Error.WriteLine($"Warning: Form field '{kvp.Key}' not found in the PDF.");
            }
        }

        // Save the modified PDF
        try
        {
            pdfDocument.Save(outputPdfPath);
            Console.WriteLine($"Form fields have been filled and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving PDF: {ex.Message}");
        }
    }
}
