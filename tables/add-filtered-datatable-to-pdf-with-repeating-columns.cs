using System;
using System.Data;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Prepare a sample DataTable with some data
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("ID", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Value", typeof(double));

        for (int i = 1; i <= 20; i++)
        {
            dataTable.Rows.Add(i, $"Item {i}", i * 10.5);
        }

        // Create a DataView and filter it to the first 5 rows
        DataView view = new DataView(dataTable);
        DataTable filteredTable = view
            .ToTable()
            .AsEnumerable()
            .Take(5)
            .CopyToDataTable();

        // Load the existing PDF, add a table, and save
        using (Document doc = new Document(inputPath))
        {
            // Create a table and configure repeating columns
            Table table = new Table
            {
                RepeatingColumnsCount = 2
            };

            // Import the filtered data into the table (include column headers)
            table.ImportDataTable(filteredTable, true, 0, 0);

            // Add the table to the first page
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and PDF saved to '{outputPath}'.");
    }
}