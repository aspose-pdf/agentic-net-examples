using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to process
        const string inputFolder = @"C:\PdfBatch";
        // Path for the generated CSV report
        const string reportPath = @"C:\PdfBatch\FieldReport.csv";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Prepare CSV writer
        using (StreamWriter writer = new StreamWriter(reportPath, false))
        {
            // CSV header
            writer.WriteLine("FileName,FieldName,FieldValue");

            // Process each PDF file in the folder
            foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
            {
                try
                {
                    // Load the PDF document (lifecycle rule: use using for deterministic disposal)
                    using (Document doc = new Document(pdfFile))
                    {
                        // Access the form (if any)
                        Form form = doc.Form;

                        // Guard against PDFs without a form
                        if (form == null || form.Count == 0)
                            continue;

                        // Iterate over all form fields
                        foreach (Field field in form)
                        {
                            // Field name (partial name) and its current value
                            string fieldName = field.PartialName ?? string.Empty;
                            string fieldValue = field.Value?.ToString() ?? string.Empty;

                            // Write a CSV line for this field
                            string line = $"{Path.GetFileName(pdfFile)},{EscapeCsv(fieldName)},{EscapeCsv(fieldValue)}";
                            writer.WriteLine(line);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{pdfFile}': {ex.Message}");
                }
            }
        }

        Console.WriteLine($"Field report generated at: {reportPath}");
    }

    // Helper to escape commas and quotes in CSV fields
    static string EscapeCsv(string input)
    {
        if (input.Contains(",") || input.Contains("\"") || input.Contains("\n"))
        {
            string escaped = input.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return input;
    }
}
