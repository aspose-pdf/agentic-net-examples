using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the template PDF and the generated output PDF
        const string templatePdfPath = "TemplateForm.pdf";
        const string outputPdfPath   = "FilledForm.pdf";

        // Verify that the template file exists
        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // Build a DataTable whose column names match the AcroForm field names
        // ------------------------------------------------------------
        DataTable dataTable = new DataTable("FormData");
        // Example field names – replace with the actual field names in your PDF
        dataTable.Columns.Add("FirstName", typeof(string));
        dataTable.Columns.Add("LastName",  typeof(string));
        dataTable.Columns.Add("Address",   typeof(string));
        dataTable.Columns.Add("City",      typeof(string));
        dataTable.Columns.Add("Country",   typeof(string));
        dataTable.Columns.Add("Agree",     typeof(bool)); // checkbox example

        // Populate the DataTable with a single row of data (add more rows as needed)
        DataRow row = dataTable.NewRow();
        row["FirstName"] = "John";
        row["LastName"]  = "Doe";
        row["Address"]   = "123 Main St.";
        row["City"]      = "Metropolis";
        row["Country"]   = "USA";
        row["Agree"]     = true; // checks the checkbox field named "Agree"
        dataTable.Rows.Add(row);

        // ------------------------------------------------------------
        // Use AutoFiller to bind the template, import the DataTable,
        // and save the filled PDF.
        // ------------------------------------------------------------
        using (AutoFiller autoFiller = new AutoFiller())
        {
            // Bind the template PDF file
            autoFiller.BindPdf(templatePdfPath);

            // Import the DataTable – column names must exactly match field names (case‑sensitive)
            autoFiller.ImportDataTable(dataTable);

            // Save the result to a single merged PDF file
            autoFiller.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF form filled and saved to '{outputPdfPath}'.");
    }
}