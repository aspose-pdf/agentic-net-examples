using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to process
        const string inputFolder = @"C:\PdfFiles";
        // Output CSV report
        const string reportPath = @"C:\PdfFiles\FormFieldsReport.csv";

        // Prepare CSV header
        var lines = new List<string>
        {
            "PdfFile,FieldName,FieldType,CharacterLimit"
        };

        // Get all PDF files in the folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            // Ensure the file exists before processing
            if (!File.Exists(pdfPath))
                continue;

            // Load the PDF document (PDF format does not require load options)
            using (Document doc = new Document(pdfPath))
            {
                // The Form property gives access to the collection of form fields
                Form form = doc.Form;

                // Iterate over each field in the form
                foreach (Field field in form)
                {
                    // Field name (the name assigned in the PDF)
                    string fieldName = field.Name ?? string.Empty;

                    // Field type (simple class name, e.g., TextBoxField, CheckBoxField)
                    string fieldType = field.GetType().Name;

                    // Character limit – many text‑based fields expose a MaxLen property.
                    // Use reflection to obtain it if present; otherwise mark as N/A.
                    int? maxLen = null;
                    PropertyInfo maxLenProp = field.GetType().GetProperty("MaxLen", BindingFlags.Public | BindingFlags.Instance);
                    if (maxLenProp != null && maxLenProp.PropertyType == typeof(int))
                    {
                        object value = maxLenProp.GetValue(field);
                        if (value != null)
                            maxLen = (int)value;
                    }

                    string charLimit = maxLen.HasValue ? maxLen.Value.ToString() : "N/A";

                    // Add a line to the CSV report
                    string csvLine = $"{Path.GetFileName(pdfPath)},{EscapeCsv(fieldName)},{fieldType},{charLimit}";
                    lines.Add(csvLine);

                    // Also write to console for immediate feedback
                    Console.WriteLine($"File: {Path.GetFileName(pdfPath)} | Name: {fieldName} | Type: {fieldType} | Limit: {charLimit}");
                }
            }
        }

        // Write all collected lines to the CSV file
        File.WriteAllLines(reportPath, lines);
        Console.WriteLine($"Report saved to: {reportPath}");
    }

    // Helper to escape commas and quotes in CSV fields
    private static string EscapeCsv(string input)
    {
        if (input.Contains(",") || input.Contains("\"") || input.Contains("\n"))
        {
            string escaped = input.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return input;
    }
}