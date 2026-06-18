using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class BatchPdfFiller
{
    /// <summary>
    /// Fills a PDF template with each DataTable in the list and creates separate output files.
    /// </summary>
    /// <param name="templatePath">Path to the PDF form template.</param>
    /// <param name="dataTables">List of DataTable objects, each containing field‑name/value pairs.</param>
    /// <param name="outputDirectory">Directory where the filled PDFs will be saved.</param>
    public static void FillTemplateBatch(string templatePath, List<DataTable> dataTables, string outputDirectory)
    {
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        for (int i = 0; i < dataTables.Count; i++)
        {
            DataTable table = dataTables[i];

            // Each AutoFiller instance works with one output PDF.
            using (AutoFiller autoFiller = new AutoFiller())
            {
                // Bind the template PDF.
                autoFiller.BindPdf(templatePath);

                // Import the data. Column names must match field names in the PDF (case‑sensitive).
                autoFiller.ImportDataTable(table);

                // Build a unique file name for this data set.
                string outputPath = Path.Combine(outputDirectory, $"filled_{i + 1}.pdf");

                // Save the filled PDF. Save(string) is the recommended overload for facades.
                autoFiller.Save(outputPath);
            }

            Console.WriteLine($"Created filled PDF: {i + 1}/{dataTables.Count}");
        }
    }

    // Example usage.
    static void Main()
    {
        // Path to the PDF form that contains fields named "CompanyName", "ContactName", etc.
        const string templatePdf = "TemplateForm.pdf";

        // Prepare a list of DataTables. In a real scenario these could come from a database.
        List<DataTable> tables = new List<DataTable>();

        // Example: first data set.
        DataTable dt1 = new DataTable("DataSet1");
        dt1.Columns.Add("CompanyName", typeof(string));
        dt1.Columns.Add("ContactName", typeof(string));
        dt1.Columns.Add("Address", typeof(string));
        dt1.Columns.Add("PostalCode", typeof(string));
        dt1.Columns.Add("City", typeof(string));
        dt1.Columns.Add("Country", typeof(string));
        dt1.Columns.Add("Heading", typeof(string));

        dt1.Rows.Add("Acme Corp", "John Doe", "123 Main St", "12345", "Metropolis", "USA", "Dear John,");
        tables.Add(dt1);

        // Example: second data set.
        DataTable dt2 = dt1.Clone(); // same schema
        dt2.Rows.Add("Globex Inc", "Jane Smith", "456 Oak Ave", "67890", "Gotham", "USA", "Dear Jane,");
        tables.Add(dt2);

        // Output folder for the generated PDFs.
        const string outputFolder = "FilledPdfs";

        // Perform the batch fill.
        FillTemplateBatch(templatePdf, tables, outputFolder);
    }
}