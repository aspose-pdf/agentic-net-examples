using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // needed for Field class

class Program
{
    static void Main()
    {
        const string pdfPath = "template.pdf";   // PDF with form fields
        const string csvPath = "data.csv";       // CSV containing field names and values
        const string outputPath = "filled.pdf";  // Resulting PDF

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

        // ------------------------------------------------------------
        // Load CSV – first line is header (field names), second line is data
        // ------------------------------------------------------------
        var fieldValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        using (StreamReader reader = new StreamReader(csvPath))
        {
            string headerLine = reader.ReadLine();
            if (headerLine == null)
            {
                Console.Error.WriteLine("CSV file is empty.");
                return;
            }

            string[] headers = headerLine.Split(',');

            string dataLine = reader.ReadLine(); // take the first data row
            if (dataLine == null)
            {
                Console.Error.WriteLine("CSV file contains no data rows.");
                return;
            }

            string[] values = dataLine.Split(',');

            int count = Math.Min(headers.Length, values.Length);
            for (int i = 0; i < count; i++)
            {
                string key = headers[i].Trim();
                string val = values[i].Trim();
                if (!string.IsNullOrEmpty(key))
                {
                    fieldValues[key] = val;
                }
            }
        }

        // ------------------------------------------------------------
        // Open PDF, populate form fields, and save
        // ------------------------------------------------------------
        using (Document doc = new Document(pdfPath))
        {
            foreach (var kvp in fieldValues)
            {
                // Check if the field exists before assigning
                if (doc.Form.HasField(kvp.Key))
                {
                    // Retrieve the field as a generic Field object and set its value
                    Field field = doc.Form[kvp.Key] as Field;
                    if (field != null)
                    {
                        field.Value = kvp.Value;
                    }
                    else
                    {
                        // Fallback for unexpected annotation types
                        Console.WriteLine($"Warning: field '{kvp.Key}' could not be cast to a Form Field.");
                    }
                }
                else
                {
                    Console.WriteLine($"Warning: field '{kvp.Key}' not found in PDF.");
                }
            }

            // Save the filled PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }
}
