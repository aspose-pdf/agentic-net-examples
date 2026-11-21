using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string csvPath   = "data.csv";      // CSV with header row = field names
        const string inputPdf  = "template.pdf"; // PDF containing form fields
        const string outputPdf = "filled.pdf";   // Resulting PDF

        // Verify input files exist
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"PDF template not found: {inputPdf}");
            return;
        }

        // -------------------------------------------------
        // Read CSV into a dictionary (header -> value)
        // -------------------------------------------------
        var fieldValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        using (StreamReader reader = new StreamReader(csvPath))
        {
            // First line: column headers (field names)
            string headerLine = reader.ReadLine();
            // Second line: values for those fields (single record assumed)
            string valueLine = reader.ReadLine();

            if (headerLine == null || valueLine == null)
            {
                Console.Error.WriteLine("CSV must contain at least two rows (header + data).");
                return;
            }

            string[] headers = headerLine.Split(',');
            string[] values  = valueLine.Split(',');

            int count = Math.Min(headers.Length, values.Length);
            for (int i = 0; i < count; i++)
            {
                string key   = headers[i].Trim();
                string value = values[i].Trim();
                if (!string.IsNullOrEmpty(key))
                    fieldValues[key] = value;
            }
        }

        // -------------------------------------------------
        // Load PDF, populate form fields, and save result
        // -------------------------------------------------
        using (Document pdfDoc = new Document(inputPdf))
        {
            Form pdfForm = pdfDoc.Form;

            foreach (var kvp in fieldValues)
            {
                // Retrieve the form field by name; returns null if not present
                Field field = pdfForm[kvp.Key] as Field;
                if (field != null)
                {
                    // The Field class (Aspose.Pdf.Forms) exposes the Value property
                    field.Value = kvp.Value;
                }
                else
                {
                    Console.WriteLine($"Warning: PDF does not contain field '{kvp.Key}'.");
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF successfully saved to '{outputPdf}'.");
    }
}
