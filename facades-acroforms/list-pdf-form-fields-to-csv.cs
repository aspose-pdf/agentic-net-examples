using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF containing form fields
        const string inputPdf = "form_document.pdf";
        // Output CSV report
        const string outputCsv = "form_fields_report.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document pdfDocument = new Document(inputPdf))
            {
                // Ensure the document actually contains a form.
                if (pdfDocument.Form == null || pdfDocument.Form.Fields == null)
                {
                    Console.WriteLine("No form fields found in the PDF.");
                    return;
                }

                // Prepare the CSV file.
                using (StreamWriter writer = new StreamWriter(outputCsv, false))
                {
                    // Write CSV header.
                    writer.WriteLine("FieldName,FieldType");

                    // Iterate over each field and write its name and type.
                    foreach (Field field in pdfDocument.Form.Fields)
                    {
                        string fieldName = field.PartialName ?? string.Empty;
                        string fieldType = field.GetType().Name; // e.g., TextBoxField, CheckBoxField, etc.
                        string line = $"{EscapeCsv(fieldName)},{EscapeCsv(fieldType)}";
                        writer.WriteLine(line);
                    }
                }

                Console.WriteLine($"Form field report written to '{outputCsv}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper to escape CSV values that may contain commas or quotes.
    static string EscapeCsv(string value)
    {
        if (value == null) return string.Empty;
        if (value.Contains('"'))
            value = value.Replace("\"", "\"\"");
        if (value.Contains(',') || value.Contains('"') || value.Contains('\n') || value.Contains('\r'))
            return $"\"{value}\"";
        return value;
    }
}
