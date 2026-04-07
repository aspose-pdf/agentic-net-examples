using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Entry point – expects PDF file paths as command‑line arguments.
    static void Main(string[] args)
    {
        // If no arguments are supplied, inform the user and exit.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: ReportFormFields <pdf1> <pdf2> ...");
            return;
        }

        // Prepare the output CSV file.
        const string outputCsv = "FormFieldsReport.csv";
        using (StreamWriter writer = new StreamWriter(outputCsv))
        {
            // Write CSV header.
            writer.WriteLine("PdfFile,FieldName,FieldType,CharacterLimit");

            // Process each PDF file supplied on the command line.
            foreach (string pdfPath in args)
            {
                if (!File.Exists(pdfPath))
                {
                    Console.Error.WriteLine($"File not found: {pdfPath}");
                    continue;
                }

                // Load the PDF document inside a using block to ensure proper disposal.
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate over all form fields in the document.
                    foreach (Field field in doc.Form)
                    {
                        // Field name (partial name) – may be empty for some fields.
                        string fieldName = field.Name ?? string.Empty;

                        // Determine the concrete field type (e.g., TextBoxField, CheckBoxField, etc.).
                        string fieldType = field.GetType().Name;

                        // Character limit is applicable mainly to text‑based fields.
                        // The MaxLen property exists on TextBoxField and its descendants.
                        int charLimit = -1; // -1 indicates “no limit / not applicable”.
                        if (field is TextBoxField txtField)
                        {
                            charLimit = txtField.MaxLen;
                        }

                        // Write a CSV line for the current field.
                        writer.WriteLine($"{Path.GetFileName(pdfPath)},{EscapeCsv(fieldName)},{fieldType},{charLimit}");
                    }
                }
            }
        }

        Console.WriteLine($"Form field report generated: {Path.GetFullPath(outputCsv)}");
    }

    // Helper to escape commas and quotes in CSV fields.
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