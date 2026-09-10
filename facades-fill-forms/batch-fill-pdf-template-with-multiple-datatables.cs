using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class BatchPdfFiller
{
    /// <summary>
    /// Fills a PDF template with each DataTable in the list.
    /// Generates separate PDF files for each data set.
    /// </summary>
    /// <param name="templatePath">Path to the PDF template containing form fields.</param>
    /// <param name="dataTables">List of DataTable objects, each representing a data set.</param>
    /// <param name="outputDirectory">Directory where the filled PDFs will be saved.</param>
    public static void FillTemplateBatch(string templatePath, List<DataTable> dataTables, string outputDirectory)
    {
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Process each DataTable
        for (int i = 0; i < dataTables.Count; i++)
        {
            DataTable table = dataTables[i];

            // AutoFiller implements IDisposable, so use a using block
            using (AutoFiller filler = new AutoFiller())
            {
                // Bind the PDF template
                filler.BindPdf(templatePath);

                // Set the path where the generated files will be placed
                filler.GeneratingPath = outputDirectory;

                // Base file name for this batch (e.g., filled_0, filled_1, ...)
                filler.BasicFileName = $"filled_{i}";

                // Import the current DataTable. Column names must match field names in the template.
                filler.ImportDataTable(table);

                // Save generates one PDF per row in the DataTable.
                // Files will be named like "filled_0_0.pdf", "filled_0_1.pdf", etc.
                filler.Save();
            }
        }

        Console.WriteLine("Batch filling completed.");
    }

    // Example usage
    static void Main()
    {
        string templatePdf = "TemplateForm.pdf";

        // Prepare example data tables
        var dataTables = new List<DataTable>();

        // First data set
        DataTable dt1 = new DataTable("DataSet1");
        dt1.Columns.Add("FirstName", typeof(string));
        dt1.Columns.Add("LastName", typeof(string));
        dt1.Columns.Add("Address", typeof(string));
        dt1.Rows.Add("John", "Doe", "123 Main St");
        dt1.Rows.Add("Jane", "Smith", "456 Oak Ave");
        dataTables.Add(dt1);

        // Second data set
        DataTable dt2 = new DataTable("DataSet2");
        dt2.Columns.Add("FirstName", typeof(string));
        dt2.Columns.Add("LastName", typeof(string));
        dt2.Columns.Add("Address", typeof(string));
        dt2.Rows.Add("Alice", "Brown", "789 Pine Rd");
        dataTables.Add(dt2);

        string outputDir = "FilledOutputs";

        FillTemplateBatch(templatePdf, dataTables, outputDir);
    }
}