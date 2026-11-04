using System;
using System.IO;
using System.Text;
using System.Linq; // Added for Count() extension method
using Aspose.Pdf;

#pragma warning disable NU1902 // Suppress known vulnerability warning for System.Security.Cryptography.Pkcs 9.0.0
#pragma warning disable NU1903 // Suppress high severity vulnerability warning for System.Security.Cryptography.Pkcs 9.0.0

class Program
{
    static void Main()
    {
        // Directory containing the PDFs to process
        const string inputDirectory = "pdfs";

        // Path for the generated summary report (CSV format)
        const string reportPath = "field_report.csv";

        // Validate input directory – fall back to current directory if missing
        string pdfFolder = Directory.Exists(inputDirectory) ? inputDirectory : Directory.GetCurrentDirectory();
        if (!Directory.Exists(pdfFolder))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // StringBuilder to accumulate CSV lines
        StringBuilder csvBuilder = new StringBuilder();

        // CSV header
        csvBuilder.AppendLine("FileName,FieldName,FieldValue");

        // Iterate over all PDF files in the input directory
        foreach (string pdfPath in Directory.GetFiles(pdfFolder, "*.pdf"))
        {
            // Ensure each Document is disposed promptly
            try
            {
                using (Document doc = new Document(pdfPath))
                {
                    // Access the form (if any) in the PDF
                    var form = doc.Form;

                    // If the PDF contains form fields, enumerate them
                    if (form != null && form.Fields != null && form.Fields.Count() > 0)
                    {
                        foreach (var field in form.Fields)
                        {
                            // FullName provides the field's identifier
                            string fieldName = field.FullName ?? string.Empty;

                            // Value may be null; convert to string safely
                            string fieldValue = field.Value?.ToString() ?? string.Empty;

                            // Escape values for CSV compliance
                            string escapedName = EscapeCsv(fieldName);
                            string escapedValue = EscapeCsv(fieldValue);

                            // Append a line: FileName,FieldName,FieldValue
                            csvBuilder.AppendLine($"{Path.GetFileName(pdfPath)},{escapedName},{escapedValue}");
                        }
                    }
                    else
                    {
                        // PDF has no form fields; still record the file with empty entries
                        csvBuilder.AppendLine($"{Path.GetFileName(pdfPath)},,");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
                // Record the file with an error indicator
                csvBuilder.AppendLine($"{Path.GetFileName(pdfPath)},Error,\"{EscapeCsv(ex.Message)}\"");
            }
        }

        // Write the accumulated CSV content to the report file
        File.WriteAllText(reportPath, csvBuilder.ToString(), Encoding.UTF8);

        Console.WriteLine($"Summary report generated: {reportPath}");
    }

    // Helper to escape commas, quotes, and newlines in CSV fields
    static string EscapeCsv(string input)
    {
        if (input.Contains(",") || input.Contains("\"") || input.Contains("\n") || input.Contains("\r"))
        {
            // Double up any existing quotes
            string escaped = input.Replace("\"", "\"\"");
            // Enclose the field in quotes
            return $"\"{escaped}\"";
        }
        return input;
    }
}
#pragma warning restore NU1902
#pragma warning restore NU1903