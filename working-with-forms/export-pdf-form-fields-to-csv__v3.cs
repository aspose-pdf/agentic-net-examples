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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Access the form object
            Form pdfForm = pdfDoc.Form;

            // Create a CSV file and write header
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false, Encoding.UTF8))
            {
                writer.WriteLine("FieldName,Value");

                // Iterate over all form fields
                foreach (Field field in pdfForm.Fields)
                {
                    // Prefer FullName; fallback to Name if FullName is null
                    string fieldName = field.FullName ?? field.Name ?? string.Empty;

                    // Get the current value of the field; handle nulls
                    string rawValue = field.Value?.ToString() ?? string.Empty;

                    // Escape CSV special characters
                    string escapedValue = rawValue.Replace("\"", "\"\"");
                    bool mustQuote = escapedValue.Contains(",") ||
                                     escapedValue.Contains("\"") ||
                                     escapedValue.Contains("\n") ||
                                     escapedValue.Contains("\r");
                    if (mustQuote)
                    {
                        escapedValue = $"\"{escapedValue}\"";
                    }

                    writer.WriteLine($"{fieldName},{escapedValue}");
                }
            }
        }

        Console.WriteLine($"Form fields exported to '{outputCsvPath}'.");
    }
}