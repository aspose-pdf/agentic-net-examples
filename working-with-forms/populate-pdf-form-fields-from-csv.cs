using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Needed for Field type

class Program
{
    static void Main()
    {
        const string inputPdfPath = "template.pdf";   // PDF with form fields
        const string csvPath      = "data.csv";       // CSV file: first row = field names, second row = values
        const string outputPdfPath = "filled.pdf";

        // Ensure source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV not found: {csvPath}");
            return;
        }

        // Load PDF document inside a using block (deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Read CSV and build a dictionary of fieldName -> fieldValue
            var fieldValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            using (StreamReader reader = new StreamReader(csvPath))
            {
                // Header line contains field names
                string headerLine = reader.ReadLine();
                if (headerLine == null)
                {
                    Console.Error.WriteLine("CSV file is empty.");
                    return;
                }
                string[] headers = headerLine.Split(',');

                // Data line contains corresponding values (assumes a single data row)
                string dataLine = reader.ReadLine();
                if (dataLine == null)
                {
                    Console.Error.WriteLine("CSV file does not contain data rows.");
                    return;
                }
                string[] values = dataLine.Split(',');

                // Populate dictionary
                int count = Math.Min(headers.Length, values.Length);
                for (int i = 0; i < count; i++)
                {
                    string key = headers[i].Trim();
                    string val = values[i].Trim();
                    if (!string.IsNullOrEmpty(key))
                        fieldValues[key] = val;
                }
            }

            // Iterate over the dictionary and set matching form fields
            foreach (var kvp in fieldValues)
            {
                // Check if the PDF contains a field with the given name
                if (pdfDoc.Form.HasField(kvp.Key))
                {
                    // The Form indexer returns a WidgetAnnotation; cast it to Field safely
                    Field? field = pdfDoc.Form[kvp.Key] as Field;
                    if (field != null)
                    {
                        // Assign the value – Field.Value is the correct property
                        field.Value = kvp.Value;
                    }
                }
                else
                {
                    // Optional: report missing fields
                    Console.WriteLine($"Warning: PDF does not contain field '{kvp.Key}'.");
                }
            }

            // Save the updated PDF
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
        }
    }
}
