using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

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

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                Form form = doc.Form;

                // Create CSV file for output
                using (StreamWriter writer = new StreamWriter(outputCsv, false, Encoding.UTF8))
                {
                    // CSV header
                    writer.WriteLine("FieldName,FieldType,DefaultValue");

                    // Iterate over all form fields
                    foreach (WidgetAnnotation widget in form)
                    {
                        // Only process fields that derive from Field to access the Value property
                        if (widget is Field field)
                        {
                            string name = field.FullName ?? string.Empty;
                            string type = field.GetType().Name;
                            string defaultValue = field.Value?.ToString() ?? string.Empty;

                            // Escape CSV special characters
                            name = EscapeCsv(name);
                            type = EscapeCsv(type);
                            defaultValue = EscapeCsv(defaultValue);

                            writer.WriteLine($"{name},{type},{defaultValue}");
                        }
                    }
                }

                Console.WriteLine($"Form field metadata exported to '{outputCsv}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper to escape commas, quotes, and newlines in CSV fields
    static string EscapeCsv(string value)
    {
        if (value.Contains("\""))
            value = value.Replace("\"", "\"\"");

        if (value.Contains(",") || value.Contains("\"") || value.Contains("\n") || value.Contains("\r"))
            value = $"\"{value}\"";

        return value;
    }
}