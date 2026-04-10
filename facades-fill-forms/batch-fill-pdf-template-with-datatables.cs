using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class BatchPdfFiller
{
    /// <summary>
    /// Fills a PDF template with each DataTable in the list and saves the results as separate PDF files.
    /// </summary>
    /// <param name="templatePath">Path to the PDF form template.</param>
    /// <param name="dataTables">List of DataTable objects, each representing a data set to fill.</param>
    /// <param name="outputFolder">Folder where the filled PDFs will be written.</param>
    public static void FillTemplateBatch(string templatePath, List<DataTable> dataTables, string outputFolder)
    {
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        // Iterate over each DataTable and generate a separate PDF file.
        for (int i = 0; i < dataTables.Count; i++)
        {
            DataTable table = dataTables[i];

            // Build a unique file name for this iteration.
            string outputFile = Path.Combine(outputFolder, $"FilledDocument_{i}.pdf");

            // AutoFiller implements IDisposable – use a using block for deterministic cleanup.
            using (AutoFiller filler = new AutoFiller())
            {
                // Bind the template PDF (new API – replaces the obsolete InputFileName property).
                filler.BindPdf(templatePath);

                // Import the current DataTable. Column names must match field names in the PDF.
                filler.ImportDataTable(table);

                // Save the filled document to the desired file path.
                filler.Save(outputFile);
            }

            Console.WriteLine($"Generated: {outputFile}");
        }
    }

    // Example usage.
    static void Main()
    {
        // Path to the PDF form that contains fillable fields.
        const string templatePdf = "template.pdf";

        // Prepare sample data tables (in real scenarios these would come from a database or other source).
        List<DataTable> tables = new List<DataTable>();

        // First data set.
        DataTable dt1 = new DataTable("DataSet1");
        dt1.Columns.Add("CompanyName", typeof(string));
        dt1.Columns.Add("ContactName", typeof(string));
        dt1.Columns.Add("Address", typeof(string));
        dt1.Columns.Add("PostalCode", typeof(string));
        dt1.Columns.Add("City", typeof(string));
        dt1.Columns.Add("Country", typeof(string));
        dt1.Rows.Add("Acme Corp", "John Doe", "123 Main St", "12345", "Metropolis", "USA");
        tables.Add(dt1);

        // Second data set.
        DataTable dt2 = new DataTable("DataSet2");
        dt2.Columns.Add("CompanyName", typeof(string));
        dt2.Columns.Add("ContactName", typeof(string));
        dt2.Columns.Add("Address", typeof(string));
        dt2.Columns.Add("PostalCode", typeof(string));
        dt2.Columns.Add("City", typeof(string));
        dt2.Columns.Add("Country", typeof(string));
        dt2.Rows.Add("Globex Inc", "Jane Smith", "456 Oak Ave", "67890", "Gotham", "USA");
        tables.Add(dt2);

        // Output directory for the generated PDFs.
        const string outputDir = "FilledOutputs";

        // Perform the batch fill operation.
        FillTemplateBatch(templatePdf, tables, outputDir);
    }
}