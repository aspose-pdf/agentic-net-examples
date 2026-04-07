using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "form_fields_audit.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create CSV file for output
            using (StreamWriter writer = new StreamWriter(outputCsv, false))
            {
                // Write CSV header
                writer.WriteLine("FieldName,PartialName,FullName,FieldType,DefaultValue,ReadOnly,Required");

                // Iterate over all form fields in the document
                foreach (WidgetAnnotation widget in doc.Form)
                {
                    // Cast to Field to access common field properties
                    Field field = widget as Field;
                    if (field == null) continue;

                    // Gather metadata
                    string name = field.Name ?? "";
                    string partial = field.PartialName ?? "";
                    string full = field.FullName ?? "";
                    string type = field.GetType().Name; // e.g., TextBoxField, CheckBoxField
                    string value = field.Value?.ToString() ?? "";
                    string readOnly = field.ReadOnly ? "True" : "False";
                    string required = field.Required ? "True" : "False";

                    // Escape values that may contain commas or quotes
                    name = EscapeCsv(name);
                    partial = EscapeCsv(partial);
                    full = EscapeCsv(full);
                    type = EscapeCsv(type);
                    value = EscapeCsv(value);

                    // Write a CSV line for the current field
                    writer.WriteLine($"{name},{partial},{full},{type},{value},{readOnly},{required}");
                }
            }
        }

        Console.WriteLine($"Form field metadata exported to '{outputCsv}'.");
    }

    // Helper method to escape CSV fields containing commas, quotes, or line breaks
    static string EscapeCsv(string s)
    {
        if (s.Contains("\""))
            s = s.Replace("\"", "\"\"");
        if (s.Contains(",") || s.Contains("\"") || s.Contains("\n") || s.Contains("\r"))
            s = $"\"{s}\"";
        return s;
    }
}