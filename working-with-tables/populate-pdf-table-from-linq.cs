using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        try
        {
            // Sample data source – a list of anonymous objects
            var people = new List<(int Id, string Name, int Age)>
            {
                (1, "Alice", 30),
                (2, "Bob",   25),
                (3, "Carol", 28)
            };

            // LINQ query – select the data we want in the table
            var query = from p in people
                        select new { p.Id, p.Name, p.Age };

            // Convert the query result to a DataTable
            DataTable dataTable = new DataTable();

            // Add columns based on the query's property names
            foreach (var prop in query.First().GetType().GetProperties())
            {
                dataTable.Columns.Add(prop.Name, prop.PropertyType);
            }

            // Add rows
            foreach (var item in query)
            {
                var row = dataTable.NewRow();
                foreach (var prop in item.GetType().GetProperties())
                {
                    row[prop.Name] = prop.GetValue(item, null) ?? DBNull.Value;
                }
                dataTable.Rows.Add(row);
            }

            // Create a new PDF document (or load an existing one)
            using (Document doc = new Document())
            {
                // Add a page to host the table
                Page page = doc.Pages.Add();

                // Create the Aspose.Pdf.Table
                Table table = new Table
                {
                    // Optional visual settings
                    Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                    DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray),
                    DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                    // Use the correct enum value for auto‑fit behavior
                    ColumnAdjustment = ColumnAdjustment.AutoFitToContent
                };

                // Import the DataTable into the Aspose.Pdf.Table
                // Parameters: (DataTable, import column names as first row, start row, start column)
                table.ImportDataTable(dataTable, true, 0, 0);

                // Position the table on the page
                table.Margin = new MarginInfo { Top = 50, Left = 50 };
                page.Paragraphs.Add(table);

                // Save the PDF
                doc.Save("output.pdf");
            }

            Console.WriteLine("PDF with populated table saved as 'output.pdf'.");
        }
        catch (FileNotFoundException ex) when (ex.FileName != null && ex.FileName.Contains("AsposePdfApi"))
        {
            Console.Error.WriteLine("Required Aspose.Pdf native library not found: " + ex.FileName);
            Console.Error.WriteLine("Make sure the corresponding AsposePdfApi_*.dll is copied to the output directory.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
