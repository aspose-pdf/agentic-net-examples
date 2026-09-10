using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputCsv = "form_fields.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document contains a form with fields
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Create (or overwrite) the CSV file
            using (StreamWriter writer = new StreamWriter(outputCsv, false, Encoding.UTF8))
            {
                // CSV header
                writer.WriteLine("FullName,PartialName,Value,ReadOnly,Required,Exportable");

                // Iterate over each form field and write its data
                foreach (Field field in doc.Form)
                {
                    string fullName   = EscapeCsv(field.FullName);
                    string partialName = EscapeCsv(field.PartialName);
                    string value       = EscapeCsv(field.Value?.ToString() ?? string.Empty);
                    string readOnly    = field.ReadOnly.ToString();
                    string required    = field.Required.ToString();
                    string exportable  = field.Exportable.ToString();

                    writer.WriteLine($"{fullName},{partialName},{value},{readOnly},{required},{exportable}");
                }
            }

            Console.WriteLine($"Form fields have been exported to '{outputCsv}'.");
        }
    }

    // Helper to escape CSV fields according to RFC 4180
    static string EscapeCsv(string input)
    {
        if (input.Contains("\""))
            input = input.Replace("\"", "\"\"");

        if (input.Contains(",") || input.Contains("\"") || input.Contains("\n") || input.Contains("\r"))
            return $"\"{input}\"";

        return input;
    }
}