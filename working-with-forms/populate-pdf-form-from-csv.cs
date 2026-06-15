using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // required for form field classes

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "template.pdf";   // PDF with form fields
        const string csvPath = "data.csv";           // CSV: FieldName,Value
        const string outputPdfPath = "filled_output.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load CSV into a dictionary (field name -> value)
        var fieldValues = LoadCsv(csvPath);

        // Open the PDF document inside a using block (ensures disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over each entry from the CSV
            foreach (var kvp in fieldValues)
            {
                string fieldName = kvp.Key;
                string fieldValue = kvp.Value;

                // Check if the form contains the field
                if (!pdfDoc.Form.HasField(fieldName))
                {
                    Console.WriteLine($"Warning: Field \"{fieldName}\" not found in PDF.");
                    continue;
                }

                // Retrieve the generic field object
                var field = pdfDoc.Form[fieldName];

                // Most form field types expose a writable "Value" property.
                // Use reflection to set it safely. For check‑boxes we also accept
                // common true/false representations.
                var valueProp = field.GetType().GetProperty("Value");
                if (valueProp != null && valueProp.CanWrite)
                {
                    try
                    {
                        if (valueProp.PropertyType == typeof(bool))
                        {
                            bool boolVal = fieldValue.Equals("true", StringComparison.OrdinalIgnoreCase) || fieldValue == "1";
                            valueProp.SetValue(field, boolVal);
                        }
                        else if (valueProp.PropertyType.IsEnum)
                        {
                            // Attempt to parse enum (e.g., for radio‑button selections)
                            var enumVal = Enum.Parse(valueProp.PropertyType, fieldValue, true);
                            valueProp.SetValue(field, enumVal);
                        }
                        else
                        {
                            // Default – assign the string (Aspose will convert as needed)
                            valueProp.SetValue(field, fieldValue);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Warning: Could not set value for field \"{fieldName}\": {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"Warning: Field type of \"{fieldName}\" does not expose a writable Value property.");
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
    }

    // Reads a CSV file where each line contains: FieldName,Value
    // Returns a dictionary of field names and their corresponding values.
    private static Dictionary<string, string> LoadCsv(string csvFilePath)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in File.ReadLines(csvFilePath))
        {
            // Skip empty lines
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Split on first comma only (allows commas inside values if quoted)
            var parts = SplitCsvLine(line);
            if (parts.Length < 2)
                continue; // malformed line – ignore

            string fieldName = parts[0].Trim();
            string fieldValue = parts[1].Trim();

            if (!string.IsNullOrEmpty(fieldName))
                dict[fieldName] = fieldValue;
        }
        return dict;
    }

    // Simple CSV splitter handling quoted fields.
    private static string[] SplitCsvLine(string line)
    {
        var result = new List<string>();
        bool inQuotes = false;
        System.Text.StringBuilder current = new System.Text.StringBuilder();

        foreach (char c in line)
        {
            if (c == '"')
            {
                inQuotes = !inQuotes;
                continue;
            }

            if (c == ',' && !inQuotes)
            {
                result.Add(current.ToString());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }
        result.Add(current.ToString());
        return result.ToArray();
    }
}
