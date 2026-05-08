using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class BatchPdfFiller
{
    static void Main(string[] args)
    {
        // Path to the PDF template that contains form fields.
        // If a full path is supplied via command‑line arguments it overrides the default.
        string templatePath = args.Length > 0 ? args[0] : "Template.pdf";

        // Verify that the template file actually exists before proceeding.
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Error: PDF template not found at '{Path.GetFullPath(templatePath)}'.");
            Console.Error.WriteLine("Make sure the file exists or provide the correct path as a command‑line argument.");
            return;
        }

        // Directory where the filled PDFs will be saved.
        const string outputDirectory = "FilledOutputs";

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDirectory);

        // Obtain the list of DataTable objects, each representing a data set.
        // This placeholder should be replaced with actual data retrieval logic.
        List<DataTable> dataSets = GetDataTables();

        // Process each DataTable and generate a filled PDF.
        for (int i = 0; i < dataSets.Count; i++)
        {
            DataTable table = dataSets[i];

            // AutoFiller implements IDisposable; use a using block for deterministic cleanup.
            using (AutoFiller filler = new AutoFiller())
            {
                // Bind the template PDF file.
                filler.BindPdf(templatePath);

                // Import the current DataTable. Column names must match field names in the template.
                filler.ImportDataTable(table);

                // Construct the output file name (e.g., filled_0.pdf, filled_1.pdf, ...).
                string outputPath = Path.Combine(outputDirectory, $"filled_{i}.pdf");

                // Save the filled PDF to the specified file.
                filler.Save(outputPath);
            }
        }

        Console.WriteLine("Batch filling completed.");
    }

    // Placeholder method to provide sample DataTables.
    // Replace this with actual logic to populate the list from a database, CSV, etc.
    private static List<DataTable> GetDataTables()
    {
        var tables = new List<DataTable>();

        // Example: create two dummy tables with matching column names.
        for (int t = 0; t < 2; t++)
        {
            DataTable dt = new DataTable($"DataSet{t}");
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("Address", typeof(string));

            // Add a single row; in real scenarios, add as many rows as needed.
            DataRow row = dt.NewRow();
            row["FirstName"] = $"John{t}";
            row["LastName"]  = $"Doe{t}";
            row["Address"]   = $"123 Main St, City{t}";
            dt.Rows.Add(row);

            tables.Add(dt);
        }

        return tables;
    }
}
