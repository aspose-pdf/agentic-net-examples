using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string csvPath = "data.csv";
        const string outputDir = "Output";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Read all lines from CSV
        string[] lines = File.ReadAllLines(csvPath);
        if (lines.Length < 2)
        {
            Console.Error.WriteLine("CSV does not contain any data rows.");
            return;
        }

        // First line contains column headers that match PDF form field names
        string[] headers = ParseCsvLine(lines[0]);

        // Process each data row
        for (int rowIndex = 1; rowIndex < lines.Length; rowIndex++)
        {
            string[] values = ParseCsvLine(lines[rowIndex]);

            if (values.Length != headers.Length)
            {
                Console.Error.WriteLine($"Row {rowIndex} column count mismatch. Skipping.");
                continue;
            }

            // Load a fresh copy of the template for each row
            using (Document doc = new Document(templatePath))
            {
                // Fill each form field with the corresponding CSV value
                for (int col = 0; col < headers.Length; col++)
                {
                    string fieldName = headers[col];
                    string fieldValue = values[col];

                    // Retrieve the field from the document's form collection
                    var field = doc.Form?.Fields?.FirstOrDefault(f => f.FullName == fieldName) as TextBoxField;
                    if (field != null)
                    {
                        field.Value = fieldValue;
                    }
                }

                // Build a unique output file name (e.g., output_1.pdf)
                string outputFileName = $"output_{rowIndex}.pdf";
                string outputPath = Path.Combine(outputDir, outputFileName);

                // Save the filled PDF
                doc.Save(outputPath);
                Console.WriteLine($"Saved: {outputPath}");
            }
        }
    }

    // Simple CSV parser that handles commas and quoted fields
    static string[] ParseCsvLine(string line)
    {
        var fields = new List<string>();
        StringBuilder current = new StringBuilder();
        bool inQuotes = false;

        foreach (char ch in line)
        {
            if (ch == '"')
            {
                inQuotes = !inQuotes;
                continue;
            }

            if (ch == ',' && !inQuotes)
            {
                fields.Add(current.ToString());
                current.Clear();
            }
            else
            {
                current.Append(ch);
            }
        }

        fields.Add(current.ToString());
        return fields.ToArray();
    }
}
