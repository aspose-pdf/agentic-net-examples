using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // Prepare a DataTable with a column that will be used for the file name
        DataTable dataTable = new DataTable("Customers");
        DataColumn nameColumn = new DataColumn("Name", typeof(string));
        DataColumn fileNameColumn = new DataColumn("FileName", typeof(string));
        dataTable.Columns.Add(nameColumn);
        dataTable.Columns.Add(fileNameColumn);

        DataRow row1 = dataTable.NewRow();
        row1["Name"] = "Alice";
        row1["FileName"] = "Invoice_Alice";
        dataTable.Rows.Add(row1);

        DataRow row2 = dataTable.NewRow();
        row2["Name"] = "Bob";
        row2["FileName"] = "Invoice_Bob";
        dataTable.Rows.Add(row2);

        // Generate a separate PDF for each row, using the FileName column as the base name
        foreach (DataRow dataRow in dataTable.Rows)
        {
            // Create a temporary table that contains only the current row
            DataTable singleRowTable = dataTable.Clone();
            singleRowTable.ImportRow(dataRow);

            AutoFiller filler = new AutoFiller();
            filler.BindPdf(templatePath);
            filler.GeneratingPath = "."; // output to current directory
            filler.BasicFileName = dataRow["FileName"].ToString();
            filler.ImportDataTable(singleRowTable);
            filler.Save(); // creates a file like "Invoice_Alice0.pdf"
            filler.Close();
        }

        Console.WriteLine("PDF files generated with custom names.");
    }
}