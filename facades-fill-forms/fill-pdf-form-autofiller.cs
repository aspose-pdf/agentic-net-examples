using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath = "filled.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine("Template PDF not found: " + templatePath);
            return;
        }

        // Create a DataTable and add columns that match the PDF form fields.
        DataTable dataTable = new DataTable("FormData");
        DataColumnCollection columns = dataTable.Columns;
        columns.Add("FirstName", typeof(string));
        columns.Add("LastName", typeof(string));
        columns.Add("Address", typeof(string));
        // Add a custom column that does not exist in the PDF form (it will be ignored).
        columns.Add("CustomNote", typeof(string));

        // Populate a single row with sample data.
        DataRow row = dataTable.NewRow();
        row["FirstName"] = "John";
        row["LastName"] = "Doe";
        row["Address"] = "123 Main St";
        row["CustomNote"] = "This column will be ignored by AutoFiller";
        dataTable.Rows.Add(row);

        // Use AutoFiller to bind the template PDF and import the DataTable.
        AutoFiller autoFiller = new AutoFiller();
        autoFiller.BindPdf(templatePath);
        autoFiller.ImportDataTable(dataTable);
        autoFiller.Save(outputPath);
        autoFiller.Close();

        Console.WriteLine("PDF form filled and saved to " + outputPath);
    }
}