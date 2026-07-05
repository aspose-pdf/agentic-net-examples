using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchPdfFiller
{
    static void Main()
    {
        // Paths – adjust as needed
        const string csvPath = "data.csv";          // CSV with data rows
        const string templatesDir = "Templates";    // Folder containing PDF templates
        const string outputDir = "Output";          // Folder for filled PDFs

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Read all lines from the CSV file
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        string[] allLines = File.ReadAllLines(csvPath);
        if (allLines.Length < 2)
        {
            Console.Error.WriteLine("CSV file must contain a header row and at least one data row.");
            return;
        }

        // Parse header – assume comma‑separated values
        string[] headers = SplitCsvLine(allLines[0]);

        // Process each data row
        for (int rowIndex = 1; rowIndex < allLines.Length; rowIndex++)
        {
            string line = allLines[rowIndex];
            if (string.IsNullOrWhiteSpace(line))
                continue; // skip empty lines

            string[] values = SplitCsvLine(line);
            if (values.Length != headers.Length)
            {
                Console.Error.WriteLine($"Row {rowIndex + 1} column count mismatch – skipping.");
                continue;
            }

            // Build a dictionary of column name → value
            var rowData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < headers.Length; i++)
                rowData[headers[i]] = values[i];

            // The CSV must contain a column named "Template" that points to the template file name
            if (!rowData.TryGetValue("Template", out string templateFileName) || string.IsNullOrWhiteSpace(templateFileName))
            {
                Console.Error.WriteLine($"Row {rowIndex + 1}: missing 'Template' column – skipping.");
                continue;
            }

            string templatePath = Path.Combine(templatesDir, templateFileName);
            if (!File.Exists(templatePath))
            {
                Console.Error.WriteLine($"Row {rowIndex + 1}: template not found – {templatePath}");
                continue;
            }

            // Determine output file name – optional "OutputFileName" column, otherwise generate one
            string outputFileName;
            if (rowData.TryGetValue("OutputFileName", out string customName) && !string.IsNullOrWhiteSpace(customName))
                outputFileName = customName;
            else
                outputFileName = Path.GetFileNameWithoutExtension(templateFileName) + $"_filled_{rowIndex}.pdf";

            string outputPath = Path.Combine(outputDir, outputFileName);

            // Load the template, fill form fields, and save the result
            using (Document pdfDoc = new Document(templatePath))
            {
                // Iterate over all columns except the control columns ("Template", "OutputFileName")
                foreach (var kvp in rowData)
                {
                    string fieldName = kvp.Key;
                    if (fieldName.Equals("Template", StringComparison.OrdinalIgnoreCase) ||
                        fieldName.Equals("OutputFileName", StringComparison.OrdinalIgnoreCase))
                        continue; // skip control columns

                    // Retrieve the form field – the indexer returns a WidgetAnnotation, so cast safely
                    var fieldObj = pdfDoc.Form[fieldName] as Field;
                    if (fieldObj == null)
                        continue; // field not found or not a form field

                    SetFieldValue(fieldObj, kvp.Value);
                }

                // Save the filled PDF – default PDF format (no SaveOptions needed)
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Row {rowIndex + 1}: generated '{outputPath}'.");
        }
    }

    // Helper that assigns a string value to the most common field types.
    private static void SetFieldValue(Field field, string value)
    {
        if (field == null)
            return;

        // Handle CheckBox fields – Aspose expects "On" / "Off"
        if (field.GetType().Name.Equals("CheckBoxField", StringComparison.OrdinalIgnoreCase))
        {
            bool isChecked = value.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                             value.Equals("1") ||
                             value.Equals("yes", StringComparison.OrdinalIgnoreCase);
            var prop = field.GetType().GetProperty("Value");
            if (prop != null && prop.CanWrite)
                prop.SetValue(field, isChecked ? "On" : "Off");
            return;
        }

        // Handle ListBox fields – replace the selection list with the supplied value
        if (field.GetType().Name.Equals("ListBoxField", StringComparison.OrdinalIgnoreCase))
        {
            var selectedItemsProp = field.GetType().GetProperty("SelectedItems");
            if (selectedItemsProp != null && selectedItemsProp.CanRead && selectedItemsProp.CanWrite)
            {
                var list = selectedItemsProp.GetValue(field) as System.Collections.IList;
                if (list != null)
                {
                    // Clear existing selections (Clear takes no arguments in IList)
                    list.Clear();
                    list.Add(value);
                }
            }
            return;
        }

        // Generic handling – most field types expose a writable "Value" property
        var valueProp = field.GetType().GetProperty("Value");
        if (valueProp != null && valueProp.CanWrite)
        {
            valueProp.SetValue(field, value);
        }
    }

    // Simple CSV line splitter that handles commas inside quoted fields
    private static string[] SplitCsvLine(string line)
    {
        var fields = new List<string>();
        bool inQuotes = false;
        StringBuilder current = new StringBuilder();

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
