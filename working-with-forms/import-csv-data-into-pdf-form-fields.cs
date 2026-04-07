using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "template.pdf";
        const string csvPath = "data.csv";
        const string outputPdf = "filled.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load CSV into a dictionary: column header -> value
        var fieldValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        using (StreamReader reader = new StreamReader(csvPath))
        {
            // Read header line
            string headerLine = reader.ReadLine();
            if (headerLine == null)
            {
                Console.Error.WriteLine("CSV file is empty.");
                return;
            }
            string[] headers = SplitCsvLine(headerLine);

            // Read first data line (assumes a single record)
            string dataLine = reader.ReadLine();
            if (dataLine == null)
            {
                Console.Error.WriteLine("CSV file does not contain data rows.");
                return;
            }
            string[] values = SplitCsvLine(dataLine);

            int count = Math.Min(headers.Length, values.Length);
            for (int i = 0; i < count; i++)
            {
                fieldValues[headers[i].Trim()] = values[i].Trim();
            }
        }

        // Open PDF and populate form fields
        using (Document doc = new Document(inputPdf))
        {
            // Turn off auto‑recalculation for performance while filling
            doc.Form.AutoRecalculate = false;

            foreach (Field field in doc.Form.Fields)
            {
                // Use the full qualified field name for matching
                string name = field.FullName;
                if (fieldValues.TryGetValue(name, out string value))
                {
                    field.Value = value; // Set field value
                }
            }

            // Re‑enable automatic recalculation. No explicit Recalculate method exists.
            doc.Form.AutoRecalculate = true;

            // Save the filled PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }

    // Simple CSV splitter (does not handle quoted commas)
    static string[] SplitCsvLine(string line)
    {
        return line.Split(',');
    }
}
