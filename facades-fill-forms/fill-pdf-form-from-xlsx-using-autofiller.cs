using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point – simulates an ASP.NET Core controller action.
    // Arguments:
    //   args[0] – path to the uploaded XLSX file (simulated upload)
    //   args[1] – path to the PDF template that contains form fields
    //   args[2] – desired output PDF file path
    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <exe> <input.xlsx> <template.pdf> <output.pdf>");
            return;
        }

        string xlsxPath = args[0];
        string templatePdfPath = args[1];
        string outputPdfPath = args[2];

        if (!File.Exists(xlsxPath))
        {
            Console.Error.WriteLine($"XLSX file not found: {xlsxPath}");
            return;
        }

        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // 1. Load data from the XLSX file into a DataTable.
            //    For brevity, this example creates a dummy DataTable.
            //    Replace this block with real XLSX parsing logic as needed.
            // -----------------------------------------------------------------
            DataTable dataTable = CreateSampleDataTable();

            // -----------------------------------------------------------------
            // 2. Use Aspose.Pdf.Facades.AutoFiller to bind the template PDF,
            //    import the DataTable and save the filled PDF.
            // -----------------------------------------------------------------
            using (AutoFiller autoFiller = new AutoFiller())
            {
                // Bind the PDF template file.
                autoFiller.BindPdf(templatePdfPath);

                // Import the data. Column names must match the field names in the PDF.
                autoFiller.ImportDataTable(dataTable);

                // Save the resulting PDF to the specified output path.
                autoFiller.Save(outputPdfPath);
            }

            Console.WriteLine($"Filled PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Creates a sample DataTable with columns that correspond to PDF form fields.
    // In a real scenario, populate the DataTable by reading the XLSX file.
    private static DataTable CreateSampleDataTable()
    {
        DataTable table = new DataTable("FormData");

        // Example field names – adjust to match your PDF template.
        table.Columns.Add("CompanyName", typeof(string));
        table.Columns.Add("ContactName", typeof(string));
        table.Columns.Add("Address", typeof(string));
        table.Columns.Add("PostalCode", typeof(string));
        table.Columns.Add("City", typeof(string));
        table.Columns.Add("Country", typeof(string));
        table.Columns.Add("Heading", typeof(string));

        // Add a single row of sample data.
        DataRow row = table.NewRow();
        row["CompanyName"] = "Acme Corp";
        row["ContactName"] = "John Doe";
        row["Address"] = "123 Main St";
        row["PostalCode"] = "12345";
        row["City"] = "Metropolis";
        row["Country"] = "USA";
        row["Heading"] = "Dear Acme Corp,";
        table.Rows.Add(row);

        return table;
    }
}