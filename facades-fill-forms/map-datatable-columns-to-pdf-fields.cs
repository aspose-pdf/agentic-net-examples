using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatePdf = "template.pdf";
        const string outputPdf   = "filled_output.pdf";

        // Verify template exists
        if (!File.Exists(templatePdf))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdf}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Build the source DataTable (replace with real data source)
        // -----------------------------------------------------------------
        DataTable sourceTable = new DataTable("SourceData");

        // Example columns – these may NOT match the PDF field names
        sourceTable.Columns.Add("CompanyName", typeof(string));
        sourceTable.Columns.Add("ContactName", typeof(string));
        sourceTable.Columns.Add("Address", typeof(string));
        sourceTable.Columns.Add("PostalCode", typeof(string));
        sourceTable.Columns.Add("City", typeof(string));
        sourceTable.Columns.Add("Country", typeof(string));
        sourceTable.Columns.Add("Heading", typeof(string));

        // Populate with sample rows
        sourceTable.Rows.Add("Acme Corp", "John Doe", "123 Main St", "12345", "Metropolis", "USA", "Dear John,");
        sourceTable.Rows.Add("Globex Inc", "Jane Smith", "456 Oak Ave", "67890", "Gotham", "USA", "Dear Jane,");

        // -----------------------------------------------------------------
        // 2. Map DataTable column names to PDF field identifiers
        // -----------------------------------------------------------------
        // Key = original column name, Value = exact field name in the PDF (case‑sensitive)
        var columnMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "CompanyName", "Company_Name" },
            { "ContactName", "Contact_Name" },
            { "Address",     "Street_Address" },
            { "PostalCode",  "ZIP_Code" },
            { "City",        "City_Name" },
            { "Country",     "Country_Name" },
            { "Heading",     "Letter_Heading" }
        };

        // Rename columns in the DataTable according to the mapping.
        // DataColumn.ColumnName can be changed directly.
        foreach (DataColumn col in sourceTable.Columns.Cast<DataColumn>().ToList())
        {
            if (columnMapping.TryGetValue(col.ColumnName, out string pdfFieldName))
            {
                col.ColumnName = pdfFieldName;
            }
        }

        // -----------------------------------------------------------------
        // 3. Use AutoFiller to merge data into the PDF template
        // -----------------------------------------------------------------
        AutoFiller autoFiller = new AutoFiller();

        // Bind the PDF template (can also bind a Document instance if preferred)
        autoFiller.BindPdf(templatePdf);

        // Import the adjusted DataTable – column names now match PDF field names
        autoFiller.ImportDataTable(sourceTable);

        // Save the result. AutoFiller handles the output stream internally.
        autoFiller.Save(outputPdf);

        // Clean up resources
        autoFiller.Close();

        Console.WriteLine($"PDF successfully generated: {outputPdf}");
    }
}