using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to process
        const string inputFolder = "InputPdfs";
        // Path for the generated summary report (CSV format)
        const string reportPath = "FieldSummaryReport.csv";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Get all PDF files in the folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        // Create (or overwrite) the report file
        using (StreamWriter writer = new StreamWriter(reportPath, false))
        {
            // CSV header
            writer.WriteLine("Document,FieldName,FieldValue");

            foreach (string pdfPath in pdfFiles)
            {
                try
                {
                    // Load each PDF document
                    using (Document doc = new Document(pdfPath))
                    {
                        Form form = doc.Form;

                        // If the document has no form fields, write an empty entry
                        if (form == null || form.Count == 0)
                        {
                            writer.WriteLine($"{Path.GetFileName(pdfPath)},,");
                            continue;
                        }

                        // Iterate over all fields and write their name/value pairs
                        foreach (var field in form.Fields)
                        {
                            string fieldName = field?.Name ?? string.Empty;
                            string fieldValue = field?.Value?.ToString() ?? string.Empty;

                            // Escape double quotes in the value for CSV compliance
                            fieldValue = fieldValue.Replace("\"", "\"\"");

                            writer.WriteLine($"{Path.GetFileName(pdfPath)},{fieldName},\"{fieldValue}\"");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log any errors but continue processing remaining files
                    Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
                }
            }
        }

        Console.WriteLine($"Summary report saved to '{reportPath}'.");
    }
}