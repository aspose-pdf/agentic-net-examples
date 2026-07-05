using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "filled_output.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // ------------------------------------------------------------
        // Build a DataTable whose column names exactly match the
        // AcroForm field names in the PDF (case‑sensitive).
        // ------------------------------------------------------------
        DataTable dataTable = new DataTable("FormData");
        dataTable.Columns.Add("FirstName", typeof(string));
        dataTable.Columns.Add("LastName",  typeof(string));
        dataTable.Columns.Add("Address",   typeof(string));
        dataTable.Columns.Add("City",      typeof(string));
        dataTable.Columns.Add("Country",   typeof(string));

        // Populate the table – replace with real data source as needed.
        DataRow row = dataTable.NewRow();
        row["FirstName"] = "John";
        row["LastName"]  = "Doe";
        row["Address"]   = "123 Main St";
        row["City"]      = "Anytown";
        row["Country"]   = "USA";
        dataTable.Rows.Add(row);

        // ------------------------------------------------------------
        // Use AutoFiller to merge the DataTable into the PDF template.
        // ------------------------------------------------------------
        AutoFiller autoFiller = new AutoFiller();
        try
        {
            // Bind the template PDF.
            autoFiller.BindPdf(templatePath);

            // Import the DataTable – column names must match field names.
            autoFiller.ImportDataTable(dataTable);

            // Save the filled PDF.
            autoFiller.Save(outputPath);
        }
        finally
        {
            // Ensure resources are released.
            autoFiller.Close();
        }

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }
}