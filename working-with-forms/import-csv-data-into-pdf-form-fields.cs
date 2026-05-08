using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath   = "template.pdf";   // PDF with form fields
        const string csvPath   = "data.csv";       // CSV file containing field values
        const string outputPdf = "filled.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV not found: {csvPath}");
            return;
        }

        // Load CSV into a dictionary: header -> value (assumes a single data row)
        Dictionary<string, string> fieldValues = LoadCsv(csvPath);

        // Open the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Iterate over all form fields and set their values if a matching entry exists
            foreach (Field field in doc.Form.Fields)
            {
                if (fieldValues.TryGetValue(field.FullName, out string value))
                {
                    field.Value = value;
                }
                else if (fieldValues.TryGetValue(field.PartialName, out value))
                {
                    // Fallback to partial name if full name not found
                    field.Value = value;
                }
            }

            // Save the populated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }

    // Simple CSV loader: expects first line as headers, second line as values
    private static Dictionary<string, string> LoadCsv(string csvFilePath)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        string[] lines = File.ReadAllLines(csvFilePath);

        if (lines.Length < 2)
            return dict; // No data

        // Split header and data rows by commas (basic CSV, no quoted commas handling)
        string[] headers = lines[0].Split(',');
        string[] values  = lines[1].Split(',');

        int count = Math.Min(headers.Length, values.Length);
        for (int i = 0; i < count; i++)
        {
            string key = headers[i].Trim();
            string val = values[i].Trim();
            if (!dict.ContainsKey(key))
                dict.Add(key, val);
        }

        return dict;
    }
}