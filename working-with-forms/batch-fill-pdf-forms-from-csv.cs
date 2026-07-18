using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string csvPath = "data.csv";               // CSV with header row
        const string templatePdfPath = "template.pdf";   // PDF template with form fields
        const string outputFolder = "FilledPdfs";

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Read CSV header
        string[] headers;
        using (StreamReader reader = new StreamReader(csvPath))
        {
            string headerLine = reader.ReadLine();
            if (headerLine == null)
            {
                Console.Error.WriteLine("CSV file is empty.");
                return;
            }
            headers = SplitCsvLine(headerLine);
            
            // Process each data row
            int rowIndex = 0;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue; // skip empty lines

                string[] values = SplitCsvLine(line);
                var fieldMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < headers.Length && i < values.Length; i++)
                {
                    fieldMap[headers[i]] = values[i];
                }

                // Determine output file name – use a column named "FileName" if present, otherwise use row index
                string outputFileName = fieldMap.ContainsKey("FileName") && !string.IsNullOrWhiteSpace(fieldMap["FileName"]) 
                    ? fieldMap["FileName"] 
                    : $"output_{rowIndex + 1}.pdf";

                string outputPath = Path.Combine(outputFolder, outputFileName);

                // Load the template, fill fields, and save
                using (Document doc = new Document(templatePdfPath))
                {
                    // Iterate over the CSV columns and set matching form fields
                    foreach (var kvp in fieldMap)
                    {
                        string fieldName = kvp.Key;
                        string fieldValue = kvp.Value;

                        // Retrieve the field; if it does not exist, skip it
                        var field = doc.Form[fieldName];
                        if (field == null)
                            continue;

                        // Set the value based on the concrete field type
                        switch (field)
                        {
                            case TextBoxField txt:
                                txt.Value = fieldValue;
                                break;
                            case CheckboxField chk:
                                // Accept "true", "1", "yes" (case‑insensitive) as checked
                                chk.Checked = fieldValue.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                                               fieldValue.Equals("1") ||
                                               fieldValue.Equals("yes", StringComparison.OrdinalIgnoreCase);
                                break;
                            case RadioButtonOptionField rad:
                                rad.Value = fieldValue;
                                break;
                            case ListBoxField listBox:
                                // Directly set the value; Aspose.PDF will select the matching item if it exists
                                listBox.Value = fieldValue;
                                break;
                            case ComboBoxField combo:
                                combo.Value = fieldValue;
                                break;
                            // Add more field types as needed
                            default:
                                // If the field type is unknown but supports the generic "Value" property via reflection, try to set it
                                var prop = field.GetType().GetProperty("Value");
                                if (prop != null && prop.CanWrite)
                                {
                                    prop.SetValue(field, fieldValue);
                                }
                                break;
                        }
                    }

                    // Save the filled PDF – PDF format does not require explicit SaveOptions
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Saved filled PDF: {outputPath}");
                rowIndex++;
            }
        }
    }

    // Simple CSV line splitter handling commas inside quoted fields
    private static string[] SplitCsvLine(string line)
    {
        var fields = new List<string>();
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
                fields.Add(current.ToString());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }

        fields.Add(current.ToString());
        return fields.ToArray();
    }
}
