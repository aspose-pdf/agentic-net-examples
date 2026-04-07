using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Folder containing the processed PDF files
        const string inputFolder = "ProcessedPdfs";
        // Path for the generated summary report (CSV format)
        const string reportPath = "FieldSummaryReport.csv";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Prepare CSV header
        using (StreamWriter writer = new StreamWriter(reportPath, false))
        {
            writer.WriteLine("DocumentName,FieldName,FieldValue");

            // Process each PDF file in the folder
            foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
            {
                string fileName = Path.GetFileName(pdfPath);

                try
                {
                    // Load the PDF document (lifecycle rule: use Document constructor)
                    using (Document doc = new Document(pdfPath))
                    {
                        // Ensure the document contains a form
                        if (doc.Form == null || doc.Form.Count == 0)
                        {
                            // No form fields in this document; skip to next file
                            continue;
                        }

                        // Iterate over all form fields
                        foreach (Field field in doc.Form.Fields)
                        {
                            // Field name (partial name) and its current value
                            string fieldName = field.PartialName ?? string.Empty;
                            string fieldValue = field.Value?.ToString() ?? string.Empty;

                            // Escape commas in values to keep CSV integrity
                            fieldName = fieldName.Replace(",", "\\,");
                            fieldValue = fieldValue.Replace(",", "\\,");

                            // Write a line for this field
                            writer.WriteLine($"{fileName},{fieldName},{fieldValue}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
                }
            }
        }

        Console.WriteLine($"Summary report generated: {reportPath}");
    }
}