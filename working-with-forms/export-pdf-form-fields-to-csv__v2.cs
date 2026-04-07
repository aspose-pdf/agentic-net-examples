using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "form_fields.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Access the form object; iterate over its Fields collection (Field objects)
            Form pdfForm = doc.Form;

            // Prepare a StreamWriter for the CSV output
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false, Encoding.UTF8))
            {
                // Write CSV header
                writer.WriteLine("FieldName,PartialName,FullName,Value,ReadOnly,Required,FieldType");

                // Iterate over each form field (Field)
                foreach (Field field in pdfForm.Fields)
                {
                    // Gather field information
                    string name = EscapeCsv(field.Name ?? string.Empty);
                    string partialName = EscapeCsv(field.PartialName ?? string.Empty);
                    string fullName = EscapeCsv(field.FullName ?? string.Empty);
                    string value = EscapeCsv(field.Value?.ToString() ?? string.Empty);
                    string readOnly = field.ReadOnly ? "True" : "False";
                    string required = field.Required ? "True" : "False";
                    string fieldType = EscapeCsv(field.GetType().Name);

                    // Write a CSV line for the current field
                    writer.WriteLine($"{name},{partialName},{fullName},{value},{readOnly},{required},{fieldType}");
                }
            }
        }

        Console.WriteLine($"Form fields exported to CSV: {outputCsvPath}");
    }

    // Helper method to escape CSV values (wrap in quotes if needed and double any internal quotes)
    private static string EscapeCsv(string input)
    {
        if (input.Contains(",") || input.Contains("\"") || input.Contains("\n") || input.Contains("\r"))
        {
            string escaped = input.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return input;
    }
}
