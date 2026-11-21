using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";
        const string outputCsv = "form_data.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Access the form fields collection
            var fields = doc.Form;
            if (fields == null || fields.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Validate that every required field has a non‑empty value
            List<string> missingRequired = new List<string>();
            foreach (Field field in fields)
            {
                // The Required property indicates a mandatory field
                if (field.Required)
                {
                    string value = field.Value?.ToString() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        missingRequired.Add(field.FullName);
                    }
                }
            }

            if (missingRequired.Count > 0)
            {
                Console.WriteLine("The following required fields are empty:");
                foreach (string name in missingRequired)
                {
                    Console.WriteLine($" - {name}");
                }
                Console.WriteLine("Export aborted to prevent incomplete submissions.");
                return;
            }

            // All required fields are filled – export the form data to CSV
            using (StreamWriter writer = new StreamWriter(outputCsv, false))
            {
                writer.WriteLine("FieldName,FieldValue");
                foreach (Field field in fields)
                {
                    // Escape double quotes in the value for CSV compliance
                    string value = field.Value?.ToString()?.Replace("\"", "\"\"") ?? string.Empty;
                    writer.WriteLine($"\"{field.FullName}\",\"{value}\"");
                }
            }

            Console.WriteLine($"Form data successfully exported to '{outputCsv}'.");
        }
    }
}