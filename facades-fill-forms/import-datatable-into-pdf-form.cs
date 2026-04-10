using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the PDF form template and the output PDF
        const string templatePdf = "FormTemplate.pdf";
        const string outputPdf   = "FilledForm.pdf";

        // ------------------------------------------------------------
        // 1. Validate that the template PDF exists
        // ------------------------------------------------------------
        if (!File.Exists(templatePdf))
        {
            Console.WriteLine($"Template PDF not found: {templatePdf}");
            return;
        }

        // ------------------------------------------------------------
        // 2. Create a DataTable that matches the form field names
        // ------------------------------------------------------------
        DataTable dataTable = new DataTable("FormData");

        // Add columns – the column names must exactly match the PDF form field names
        DataColumn colName = dataTable.Columns.Add("FirstName", typeof(string));
        DataColumn colAge  = dataTable.Columns.Add("Age", typeof(int));

        // ------------------------------------------------------------
        // 3. Configure column properties before importing
        // ------------------------------------------------------------
        // Make the 'FirstName' column read‑only – its values cannot be changed after import
        colName.ReadOnly = true;

        // Ensure that each 'Age' value is unique across rows
        colAge.Unique = true;

        // ------------------------------------------------------------
        // 4. Populate the DataTable with sample data
        // ------------------------------------------------------------
        dataTable.Rows.Add("John", 30);
        dataTable.Rows.Add("Jane", 31);
        // The following line would throw an exception because 'Age' must be unique
        // dataTable.Rows.Add("Bob", 30);

        // ------------------------------------------------------------
        // 5. Use AutoFiller (Aspose.Pdf.Facades) to import the table into the PDF form
        // ------------------------------------------------------------
        AutoFiller autoFiller = new AutoFiller();

        // Bind the PDF form template – replaces the obsolete InputFileName property
        autoFiller.BindPdf(templatePdf);

        // Import the configured DataTable – column names must match form field names
        autoFiller.ImportDataTable(dataTable);

        // Save the filled PDF to the desired output file
        autoFiller.Save(outputPdf);

        Console.WriteLine($"Filled PDF saved to: {outputPdf}");
    }
}