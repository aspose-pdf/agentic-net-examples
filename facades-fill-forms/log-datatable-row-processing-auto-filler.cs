using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF template that contains form fields matching the DataTable column names
        const string templatePath = "template.pdf";

        // Directory where the generated PDFs will be placed
        const string outputDir = "Output";

        // Base name for the generated files (e.g., Output\Document0.pdf, Output\Document1.pdf, ...)
        const string baseFileName = "Document";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // -------------------------------------------------
        // Build a sample DataTable – in real scenarios this would come from a database or other source
        // -------------------------------------------------
        DataTable dataTable = new DataTable("Data");
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Address", typeof(string));
        dataTable.Columns.Add("Amount", typeof(decimal));

        dataTable.Rows.Add("Alice", "123 Main St", 100.5m);
        dataTable.Rows.Add("Bob", "456 Oak Ave", 250.0m);
        dataTable.Rows.Add("Charlie", "789 Pine Rd", 75.25m);
        // -------------------------------------------------

        // -------------------------------------------------
        // Prepare a simple logger that writes to a file inside the output folder
        // -------------------------------------------------
        string logPath = Path.Combine(outputDir, "generation.log");
        using (StreamWriter logWriter = new StreamWriter(logPath, append: false))
        {
            // Use AutoFiller (Aspose.Pdf.Facades) to generate one PDF per DataTable row
            using (AutoFiller autoFiller = new AutoFiller())
            {
                // Load the template PDF
                if (!File.Exists(templatePath))
                {
                    Console.WriteLine($"Template file not found: {templatePath}");
                    return;
                }
                autoFiller.BindPdf(templatePath);

                // Configure output to generate many small files – each file will contain a single page (the filled template)
                autoFiller.GeneratingPath = outputDir + Path.DirectorySeparatorChar;
                autoFiller.BasicFileName = baseFileName;

                // Log each row before it is processed and record the page number (always 1 for this scenario)
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    string logEntry = $"Processing row {i + 1}/{dataTable.Rows.Count}: " +
                                      $"Name={row["Name"]}, Address={row["Address"]}, Amount={row["Amount"]} => Page=1";
                    Console.WriteLine(logEntry);
                    logWriter.WriteLine(logEntry);
                }

                // Import the whole DataTable; AutoFiller will create one PDF per row.
                autoFiller.ImportDataTable(dataTable);

                // Save all generated PDFs to the specified folder using the non‑obsolete overload.
                // The overload expects a destination folder when many files are generated.
                autoFiller.Save(outputDir);
            }
        }

        Console.WriteLine("PDF generation completed. Log written to " + logPath);
    }
}
