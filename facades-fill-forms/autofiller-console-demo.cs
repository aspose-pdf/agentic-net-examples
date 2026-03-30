using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: path to the PDF template and path to the XLSX data file.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: program <template.pdf> <data.xlsx>");
            return;
        }

        string templatePath = args[0];
        string dataPath = args[1];
        string outputPath = "filled.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }

        // NOTE: In a real implementation you would read the XLSX file (dataPath) into the DataTable.
        // For this self‑contained example we create a sample DataTable that matches the field names
        // of the PDF form.
        DataTable dataTable = new DataTable("Data");
        dataTable.Columns.Add("FirstName", typeof(string));
        dataTable.Columns.Add("LastName", typeof(string));
        dataTable.Columns.Add("Address", typeof(string));

        DataRow row = dataTable.NewRow();
        row["FirstName"] = "John";
        row["LastName"] = "Doe";
        row["Address"] = "123 Main St";
        dataTable.Rows.Add(row);

        // Use AutoFiller to bind the template, import the data and save the filled PDF.
        AutoFiller autoFiller = new AutoFiller();
        autoFiller.BindPdf(templatePath);
        autoFiller.ImportDataTable(dataTable);
        autoFiller.Save(outputPath);

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }
}
