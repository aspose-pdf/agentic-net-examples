using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // ------------------------------------------------------------
        // Build a sample DataTable that mimics data coming from a source.
        // ------------------------------------------------------------
        DataTable sourceTable = new DataTable("Data");
        sourceTable.Columns.Add("CustomerName", typeof(string));
        sourceTable.Columns.Add("InvoiceAmount", typeof(decimal));
        sourceTable.Columns.Add("InvoiceDate", typeof(DateTime));

        sourceTable.Rows.Add("Acme Corp", 1234.567m, DateTime.Today);
        sourceTable.Rows.Add("Beta Ltd",   89.1m,   DateTime.Today.AddDays(1));

        // ------------------------------------------------------------
        // Create a new DataTable where numeric values are converted to
        // formatted strings before they are sent to the PDF fields.
        // ------------------------------------------------------------
        DataTable formattedTable = sourceTable.Clone();

        // Change the type of numeric columns to string so we can store
        // the formatted representation.
        formattedTable.Columns["InvoiceAmount"].DataType = typeof(string);
        formattedTable.Columns["InvoiceDate"].DataType   = typeof(string);

        foreach (DataRow srcRow in sourceTable.Rows)
        {
            DataRow dstRow = formattedTable.NewRow();

            // Copy text fields unchanged.
            dstRow["CustomerName"] = srcRow["CustomerName"];

            // Format decimal with two fractional digits and thousand separator.
            decimal amount = (decimal)srcRow["InvoiceAmount"];
            dstRow["InvoiceAmount"] = amount.ToString("N2"); // e.g., "1,234.57"

            // Format date in ISO style (yyyy-MM-dd).
            DateTime date = (DateTime)srcRow["InvoiceDate"];
            dstRow["InvoiceDate"] = date.ToString("yyyy-MM-dd");

            formattedTable.Rows.Add(dstRow);
        }

        // ------------------------------------------------------------
        // Use AutoFiller to bind the template PDF, import the formatted
        // DataTable, and save the resulting document.
        // ------------------------------------------------------------
        using (AutoFiller autoFiller = new AutoFiller())
        {
            // Bind the PDF template.
            autoFiller.BindPdf(templatePath);

            // Import the pre‑formatted data. Column names must match field names.
            autoFiller.ImportDataTable(formattedTable);

            // Save the filled PDF.
            autoFiller.Save(outputPath);
        }

        Console.WriteLine($"PDF generated: {outputPath}");
    }
}