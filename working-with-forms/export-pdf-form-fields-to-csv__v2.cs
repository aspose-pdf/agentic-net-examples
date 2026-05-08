using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Added for WidgetAnnotation

class ExportFormFieldsToCsv
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

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document actually contains a form
            if (pdfDoc.Form == null || pdfDoc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the PDF.");
                return;
            }

            // Open a CSV file for writing (UTF‑8 with BOM for Excel compatibility)
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false, new System.Text.UTF8Encoding(true)))
            {
                // Write CSV header
                writer.WriteLine("FieldName,Value");

                // Helper to escape CSV values
                string Escape(string s)
                {
                    if (s.Contains('"'))
                        s = s.Replace("\"", "\"\"");
                    if (s.Contains(',') || s.Contains('"') || s.Contains('\n') || s.Contains('\r'))
                        s = $"\"{s}\"";
                    return s;
                }

                // Iterate over all form fields
                foreach (Field field in pdfDoc.Form.Fields)
                {
                    // Field name – use FullName if available, otherwise fallback to Name
                    var widget = field as WidgetAnnotation;
                    string fieldName = widget?.FullName ?? field.Name ?? string.Empty;

                    // Field value – convert to string, handling nulls
                    string fieldValue = field.Value?.ToString() ?? string.Empty;

                    writer.WriteLine($"{Escape(fieldName)},{Escape(fieldValue)}");
                }
            }

            Console.WriteLine($"Form fields exported to CSV: {outputCsvPath}");
        }
    }
}
