using System;
using System.Data;
using System.IO;

class Program
{
    static void Main()
    {
        // Paths to the template PDF and the output PDF
        string templatePath = "template.pdf";
        string outputPath   = "filled.pdf";

        // Verify that the template file exists
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Create a DataTable and add columns that correspond to the
        //    field names in the PDF form. Custom columns can be added
        //    here before the import operation.
        // ------------------------------------------------------------
        DataTable dataTable = new DataTable("FormData");
        DataColumnCollection columns = dataTable.Columns;

        // Standard form fields
        columns.Add("FirstName", typeof(string));
        columns.Add("LastName",  typeof(string));
        columns.Add("Address",   typeof(string));
        columns.Add("City",      typeof(string));
        columns.Add("Country",   typeof(string));

        // Custom column required by the PDF form
        columns.Add("CustomNote", typeof(string));

        // ------------------------------------------------------------
        // 2. Populate the DataTable with sample data (one row in this case)
        // ------------------------------------------------------------
        DataRow row = dataTable.NewRow();
        row["FirstName"]  = "John";
        row["LastName"]   = "Doe";
        row["Address"]    = "123 Main St";
        row["City"]       = "Metropolis";
        row["Country"]    = "USA";
        row["CustomNote"] = "Important customer";
        dataTable.Rows.Add(row);

        // ------------------------------------------------------------
        // 3. Use AutoFiller to bind the template PDF, import the DataTable,
        //    and save the filled PDF.
        // ------------------------------------------------------------
        Aspose.Pdf.Facades.AutoFiller autoFiller = new Aspose.Pdf.Facades.AutoFiller();
        autoFiller.BindPdf(templatePath);          // Load the template PDF
        autoFiller.ImportDataTable(dataTable);    // Import data into matching fields
        autoFiller.Save(outputPath);              // Save the resulting PDF
        autoFiller.Close();                       // Release resources

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }
}