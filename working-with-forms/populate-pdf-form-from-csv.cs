using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string csvPath = "data.csv";          // CSV with header row matching PDF field names
        const string pdfPath = "template.pdf";      // PDF containing form fields
        const string outputPdf = "filled_output.pdf";

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfPath}");
            return;
        }

        // Read CSV file
        List<Dictionary<string, string>> records = new List<Dictionary<string, string>>();
        using (StreamReader reader = new StreamReader(csvPath))
        {
            string headerLine = reader.ReadLine();
            if (headerLine == null)
            {
                Console.Error.WriteLine("CSV file is empty.");
                return;
            }

            // Split header into column names
            string[] headers = SplitCsvLine(headerLine);

            // Read each data line and map column name -> value
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] values = SplitCsvLine(line);
                var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < headers.Length && i < values.Length; i++)
                {
                    dict[headers[i]] = values[i];
                }
                records.Add(dict);
            }
        }

        if (records.Count == 0)
        {
            Console.Error.WriteLine("CSV contains no data rows.");
            return;
        }

        // For this example we populate the PDF with the first record only.
        var firstRecord = records[0];

        // Load PDF template and fill form fields
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Ensure the document contains a form
            if (pdfDoc.Form == null || pdfDoc.Form.Count == 0)
            {
                Console.Error.WriteLine("PDF does not contain any form fields.");
                return;
            }

            // Iterate over each field in the PDF form (use Form.Fields collection)
            foreach (Field field in pdfDoc.Form.Fields)
            {
                // Prefer FullName, fall back to Name if FullName is null/empty
                string fieldName = !string.IsNullOrEmpty(field.FullName) ? field.FullName : field.Name;

                if (string.IsNullOrEmpty(fieldName))
                    continue; // skip unnamed fields

                // If CSV provides a value for this field, set it
                if (firstRecord.TryGetValue(fieldName, out string value))
                {
                    // All form field types expose the Value property (object).
                    field.Value = value;
                }
            }

            // Save the populated PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF populated and saved to '{outputPdf}'.");
    }

    // Simple CSV line splitter handling commas and quoted values
    static string[] SplitCsvLine(string line)
    {
        var values = new List<string>();
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
                values.Add(current.ToString());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }

        values.Add(current.ToString());
        return values.ToArray();
    }
}
