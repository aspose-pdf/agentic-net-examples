using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "template.pdf";   // PDF with form fields
        const string outputPdfPath  = "filled.pdf";     // Resulting PDF
        const string configFilePath = "defaultValues.txt"; // Each line: FieldName=Value

        // Load default values from configuration file
        var defaultValues = new Dictionary<string, string>(StringComparer.Ordinal);
        if (File.Exists(configFilePath))
        {
            foreach (var line in File.ReadAllLines(configFilePath))
            {
                // Skip empty lines and comments
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                    continue;

                var parts = line.Split(new[] { '=' }, 2);
                if (parts.Length == 2)
                {
                    var fieldName = parts[0].Trim();
                    var fieldValue = parts[1].Trim();
                    if (!string.IsNullOrEmpty(fieldName))
                        defaultValues[fieldName] = fieldValue;
                }
            }
        }
        else
        {
            Console.Error.WriteLine($"Configuration file not found: {configFilePath}");
            return;
        }

        // Open the PDF document and bind it to the Form facade
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            Form form = new Form(pdfDoc); // Facade works on the opened document

            // Fill each field with its default value
            foreach (var kvp in defaultValues)
            {
                string fieldName  = kvp.Key;
                string fieldValue = kvp.Value;

                // Attempt to fill the field; ignore if the field does not exist
                bool filled = form.FillField(fieldName, fieldValue);
                if (!filled)
                {
                    Console.WriteLine($"Warning: Field '{fieldName}' not found or could not be filled.");
                }
            }

            // Save the updated PDF
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields populated and saved to '{outputPdfPath}'.");
    }
}