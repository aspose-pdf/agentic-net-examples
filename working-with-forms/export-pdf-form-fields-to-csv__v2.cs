using System;
using System.IO;
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

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Open a CSV writer
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                // Write CSV header
                writer.WriteLine("FullName,PartialName,Value,PageIndex,Rect");

                // Iterate over all form fields
                foreach (Field field in doc.Form.Fields)
                {
                    // Common field properties
                    string fullName = field.FullName ?? string.Empty;
                    string partialName = field.PartialName ?? string.Empty;
                    string value = field.Value?.ToString() ?? string.Empty;

                    // A Field itself is a WidgetAnnotation, so we can access visual properties directly
                    int pageIndex = field.PageIndex; // 1‑based page index
                    Aspose.Pdf.Rectangle rect = field.Rect;
                    string rectStr = $"{rect.LLX},{rect.LLY},{rect.URX},{rect.URY}";

                    // Escape commas and quotes for CSV compliance
                    string escFullName = EscapeCsv(fullName);
                    string escPartialName = EscapeCsv(partialName);
                    string escValue = EscapeCsv(value);
                    string escRect = EscapeCsv(rectStr);

                    // Write CSV line for this field
                    writer.WriteLine($"{escFullName},{escPartialName},{escValue},{pageIndex},{escRect}");
                }
            }
        }

        Console.WriteLine($"Form fields exported to '{outputCsvPath}'.");
    }

    // Helper to escape CSV fields containing commas, quotes or new lines
    static string EscapeCsv(string field)
    {
        if (field.Contains('"'))
            field = field.Replace("\"", "\"\"");

        if (field.Contains(',') || field.Contains('"') || field.Contains('\n'))
            field = $"\"{field}\"";

        return field;
    }
}
