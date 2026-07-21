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
        // AcroForm field names in the PDF template (case‑sensitive).
        // ------------------------------------------------------------
        DataTable dataTable = new DataTable("FormData");
        dataTable.Columns.Add("FirstName", typeof(string));
        dataTable.Columns.Add("LastName",  typeof(string));
        dataTable.Columns.Add("Address",   typeof(string));
        dataTable.Columns.Add("City",      typeof(string));
        dataTable.Columns.Add("PostalCode",typeof(string));

        // Add one (or more) rows of data.
        DataRow row = dataTable.NewRow();
        row["FirstName"] = "John";
        row["LastName"]  = "Doe";
        row["Address"]   = "123 Main St.";
        row["City"]      = "Metropolis";
        row["PostalCode"]= "12345";
        dataTable.Rows.Add(row);

        // ------------------------------------------------------------
        // Use AutoFiller to bind the template, import the DataTable,
        // and save the filled PDF.
        // ------------------------------------------------------------
        Aspose.Pdf.Facades.AutoFiller autoFiller = new Aspose.Pdf.Facades.AutoFiller();

        // Bind the PDF template file.
        autoFiller.BindPdf(templatePath);

        // Import the DataTable; column names must match field names.
        autoFiller.ImportDataTable(dataTable);

        // Save the resulting PDF.
        autoFiller.Save(outputPath);

        // Release resources.
        autoFiller.Close();

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }
}