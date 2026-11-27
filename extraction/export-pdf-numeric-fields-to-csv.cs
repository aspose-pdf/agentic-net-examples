using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string csvPath = "numeric_fields.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Prepare CSV writer
            using (StreamWriter writer = new StreamWriter(csvPath, false, Encoding.UTF8))
            {
                // Write CSV header
                writer.WriteLine("FieldName,Value");

                // Regular expression to detect any numeric character
                Regex hasDigit = new Regex(@"\d", RegexOptions.Compiled);

                // Iterate over all form fields
                foreach (var field in doc.Form.Fields)
                {
                    // Some fields may have null values; treat them as empty strings
                    string value = field.Value?.ToString() ?? string.Empty;

                    // If the value contains at least one digit, write it to CSV
                    if (hasDigit.IsMatch(value))
                    {
                        // Escape commas in the value by surrounding with double quotes
                        string escapedValue = value.Contains(",") ? $"\"{value.Replace("\"", "\"\"")}\"" : value;
                        writer.WriteLine($"{field.FullName},{escapedValue}");
                    }
                }
            }
        }

        Console.WriteLine($"CSV of numeric fields saved to '{csvPath}'.");
    }
}