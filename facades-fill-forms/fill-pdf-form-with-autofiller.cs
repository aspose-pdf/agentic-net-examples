using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatePdfPath = "template.pdf";
        const string outputPdfPath   = "filled_output.pdf";

        // Verify template exists
        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Create a DataTable that matches the PDF form fields.
        // ------------------------------------------------------------
        DataTable dataTable = new DataTable("FormData");
        DataColumnCollection columns = dataTable.Columns;

        // Add columns – names must exactly match the field names in the PDF form
        columns.Add("FirstName", typeof(string));
        columns.Add("LastName",  typeof(string));
        columns.Add("Address",   typeof(string));
        columns.Add("City",      typeof(string));
        columns.Add("Country",   typeof(string));
        columns.Add("PostalCode",typeof(string));
        // Example of a custom column that does not exist in the original form
        // (will be ignored by AutoFiller if no matching field)
        columns.Add("CustomNote", typeof(string));

        // ------------------------------------------------------------
        // 2. Populate the DataTable with sample data.
        // ------------------------------------------------------------
        DataRow row = dataTable.NewRow();
        row["FirstName"]  = "John";
        row["LastName"]   = "Doe";
        row["Address"]    = "123 Main St.";
        row["City"]       = "Metropolis";
        row["Country"]    = "USA";
        row["PostalCode"] = "12345";
        row["CustomNote"] = "This column has no matching field in the PDF.";
        dataTable.Rows.Add(row);

        // ------------------------------------------------------------
        // 3. Use AutoFiller to bind the template, import the data, and save.
        // ------------------------------------------------------------
        try
        {
            AutoFiller autoFiller = new AutoFiller();

            // Bind the PDF template
            autoFiller.BindPdf(templatePdfPath);

            // Import the DataTable – column names must match field names
            autoFiller.ImportDataTable(dataTable);

            // Save the merged result to a single PDF file
            autoFiller.Save(outputPdfPath);

            // Clean up resources used by AutoFiller
            autoFiller.Close();

            Console.WriteLine($"PDF form filled and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during form filling: {ex.Message}");
        }
    }
}