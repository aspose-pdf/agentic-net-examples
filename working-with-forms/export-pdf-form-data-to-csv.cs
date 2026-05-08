using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Escapes a CSV field by wrapping it in quotes if needed and escaping internal quotes.
    static string EscapeCsv(string field)
    {
        if (field == null) return "";
        bool mustQuote = field.Contains(',') || field.Contains('\"') || field.Contains('\n') || field.Contains('\r');
        if (mustQuote)
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";      // PDF containing form fields
        const string outputCsvPath = "formdata.csv";  // Desired CSV output

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export form fields to JSON using Aspose.Pdf's ExportToJson method
            using (MemoryStream jsonStream = new MemoryStream())
            {
                pdfDoc.Form.ExportToJson(jsonStream);
                jsonStream.Position = 0; // Reset stream for reading

                // Parse the exported JSON
                using (JsonDocument jsonDoc = JsonDocument.Parse(jsonStream))
                {
                    JsonElement root = jsonDoc.RootElement;

                    // Write CSV file
                    using (StreamWriter csvWriter = new StreamWriter(outputCsvPath, false, Encoding.UTF8))
                    {
                        // Header row
                        csvWriter.WriteLine("FieldName,FieldValue");

                        // Expecting an array of field objects
                        foreach (JsonElement fieldElement in root.EnumerateArray())
                        {
                            // The JSON structure contains "FullName" and "Value" properties
                            string name = fieldElement.GetProperty("FullName").GetString();
                            string value = fieldElement.GetProperty("Value").GetString();

                            // Escape values for CSV compliance
                            name = EscapeCsv(name);
                            value = EscapeCsv(value);

                            csvWriter.WriteLine($"{name},{value}");
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Form data exported to CSV: {outputCsvPath}");
    }
}