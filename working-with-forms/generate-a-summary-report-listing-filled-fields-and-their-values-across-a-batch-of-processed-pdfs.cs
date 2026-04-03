using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Directory containing the PDFs to process
        // Use a path that works on any OS. If the folder does not exist, report and exit.
        const string pdfDirectory = "PdfBatch"; // relative to the current working directory
        // Output CSV report path (placed in the same folder)
        const string reportFileName = "FieldReport.csv";

        // Resolve absolute paths
        string basePath = Path.GetFullPath(pdfDirectory);
        string reportPath = Path.Combine(basePath, reportFileName);

        if (!Directory.Exists(basePath))
        {
            Console.Error.WriteLine($"Directory not found: {basePath}");
            return;
        }

        // Get all PDF files in the directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(basePath, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.Error.WriteLine("No PDF files found in the specified directory.");
            return;
        }

        // Create the CSV report file
        using (StreamWriter writer = new StreamWriter(reportPath, false))
        {
            // Write CSV header
            writer.WriteLine("FileName,FieldName,FieldValue");

            // Process each PDF
            foreach (string pdfPath in pdfFiles)
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Ensure the document contains a form
                    Form form = doc.Form;
                    if (form == null || form.Count == 0)
                    {
                        // No fields – write a placeholder line
                        writer.WriteLine($"{Path.GetFileName(pdfPath)},(no fields),");
                        continue;
                    }

                    // Iterate over all fields in the form
                    foreach (Field field in form.Fields)
                    {
                        // Field name (partial name) and its current value
                        string fieldName = field.PartialName ?? string.Empty;
                        string fieldValue = field.Value?.ToString() ?? string.Empty;

                        // Escape commas in values for CSV (simple backslash escape)
                        fieldName = fieldName.Replace(",", "\\,");
                        fieldValue = fieldValue.Replace(",", "\\,");

                        writer.WriteLine($"{Path.GetFileName(pdfPath)},{fieldName},{fieldValue}");
                    }
                }
            }
        }

        Console.WriteLine($"Field summary report generated at: {reportPath}");
    }
}
