using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "template.pdf";
        const string csvPath = "data.csv";
        const string outputPath = "filled.pdf";

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

        // Read CSV (first row = field names, second row = values)
        var fieldValues = ReadCsvIntoDictionary(csvPath);

        // Load PDF and fill form fields
        using (Document doc = new Document(pdfPath))
        {
            Form form = doc.Form;

            foreach (var kvp in fieldValues)
            {
                // Check if the field exists in the PDF
                if (form.HasField(kvp.Key))
                {
                    // The Form indexer returns a WidgetAnnotation; cast it to Field safely.
                    Field? field = form[kvp.Key] as Field;
                    if (field != null)
                    {
                        field.Value = kvp.Value;
                    }
                    else
                    {
                        Console.WriteLine($"Field '{kvp.Key}' exists but could not be cast to a form field.");
                    }
                }
                else
                {
                    Console.WriteLine($"Field '{kvp.Key}' not found in PDF.");
                }
            }

            // Optional: flatten the form to make fields non‑editable
            // doc.Flatten();

            // Save the populated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    // Reads a simple CSV file into a dictionary (header/value pairs)
    static Dictionary<string, string> ReadCsvIntoDictionary(string csvFile)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        using (StreamReader reader = new StreamReader(csvFile))
        {
            string headerLine = reader.ReadLine();
            string valueLine = reader.ReadLine();

            if (headerLine == null || valueLine == null)
                return dict;

            var headers = SplitCsvLine(headerLine);
            var values = SplitCsvLine(valueLine);

            int count = Math.Min(headers.Length, values.Length);
            for (int i = 0; i < count; i++)
            {
                dict[headers[i].Trim()] = values[i].Trim();
            }
        }
        return dict;
    }

    // Basic CSV splitter that handles quoted fields
    static string[] SplitCsvLine(string line)
    {
        var parts = new List<string>();
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
                parts.Add(current.ToString());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }
        parts.Add(current.ToString());
        return parts.ToArray();
    }
}
