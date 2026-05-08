using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatePdfPath = "template.pdf";   // PDF with form fields
        const string outputPdfPath   = "filled_output.pdf";

        // Verify template exists
        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }

        // --------------------------------------------------------------------
        // 1. Build the source DataTable (could be loaded from DB, CSV, etc.)
        // --------------------------------------------------------------------
        DataTable sourceTable = new DataTable("SourceData");

        // Example columns – replace with your actual source column names
        sourceTable.Columns.Add("FirstName", typeof(string));
        sourceTable.Columns.Add("LastName",  typeof(string));
        sourceTable.Columns.Add("Email",    typeof(string));

        // Populate with sample data
        sourceTable.Rows.Add("John", "Doe", "john.doe@example.com");
        sourceTable.Rows.Add("Jane", "Smith", "jane.smith@example.com");

        // --------------------------------------------------------------------
        // 2. Map source column names to PDF field identifiers
        //    (When automatic mapping fails, rename columns to match field names)
        // --------------------------------------------------------------------
        // Key   = existing column name in the DataTable
        // Value = exact field name in the PDF (case‑sensitive)
        var columnToFieldMap = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            { "FirstName", "txtFirstName" },
            { "LastName",  "txtLastName"  },
            { "Email",     "txtEmail"     }
        };

        foreach (var kvp in columnToFieldMap)
        {
            if (sourceTable.Columns.Contains(kvp.Key))
            {
                // Rename the column so that it matches the PDF field identifier
                sourceTable.Columns[kvp.Key].ColumnName = kvp.Value;
            }
            else
            {
                Console.Error.WriteLine($"Warning: source column '{kvp.Key}' not found in DataTable.");
            }
        }

        // --------------------------------------------------------------------
        // 3. Use AutoFiller to bind the PDF, import the adjusted DataTable,
        //    and save the resulting document.
        // --------------------------------------------------------------------
        using (AutoFiller autoFiller = new AutoFiller())
        {
            // Bind the template PDF
            autoFiller.BindPdf(templatePdfPath);

            // Import the DataTable – column names now match PDF field names
            autoFiller.ImportDataTable(sourceTable);

            // Save the filled PDF
            autoFiller.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF filled and saved to '{outputPdfPath}'.");
    }
}