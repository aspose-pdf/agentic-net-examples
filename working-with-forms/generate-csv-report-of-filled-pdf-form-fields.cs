using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // List of PDF files to process
        string[] pdfFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };

        // Prepare CSV header
        var reportLines = new List<string>
        {
            "File,FieldName,FieldValue"
        };

        foreach (var pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Export form fields to JSON using the Form API
                using (MemoryStream ms = new MemoryStream())
                {
                    doc.Form.ExportToJson(ms);
                    ms.Position = 0;

                    using (StreamReader reader = new StreamReader(ms))
                    {
                        string json = reader.ReadToEnd();

                        // Deserialize JSON into a list of field info objects
                        try
                        {
                            var fields = JsonSerializer.Deserialize<List<FormFieldInfo>>(json);
                            if (fields != null)
                            {
                                foreach (var field in fields)
                                {
                                    // Include only fields that have a value
                                    if (!string.IsNullOrEmpty(field.Value))
                                    {
                                        string line = $"{Path.GetFileName(pdfPath)},{field.Name},{field.Value}";
                                        reportLines.Add(line);
                                    }
                                }
                            }
                        }
                        catch (JsonException ex)
                        {
                            Console.Error.WriteLine($"JSON parse error for '{pdfPath}': {ex.Message}");
                        }
                    }
                }
            }
        }

        // Write the summary report to a CSV file
        const string reportPath = "FormFieldsReport.csv";
        File.WriteAllLines(reportPath, reportLines, Encoding.UTF8);
        Console.WriteLine($"Summary report saved to '{reportPath}'.");
    }

    // Helper class matching the JSON structure produced by ExportToJson
    private class FormFieldInfo
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}