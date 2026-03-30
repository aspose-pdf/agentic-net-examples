using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputPath = "template.pdf";
        string outputPath = "filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Prepare sample data for the form fields
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("FirstName", typeof(string));
        dataTable.Columns.Add("LastName", typeof(string));
        DataRow row = dataTable.NewRow();
        row["FirstName"] = "John";
        row["LastName"] = "Doe";
        dataTable.Rows.Add(row);

        // Fill the PDF form while preserving its original layout
        AutoFiller autoFiller = new AutoFiller();
        autoFiller.BindPdf(inputPath);
        autoFiller.ImportDataTable(dataTable);
        autoFiller.Save(outputPath);

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }
}